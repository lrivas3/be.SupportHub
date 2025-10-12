using AutoMapper;
using HelpPoint.Common.Errors.Exceptions;
using HelpPoint.Features.Auth;
using HelpPoint.Features.Support;
using HelpPoint.Infrastructure.Dtos;
using HelpPoint.Infrastructure.Dtos.Request;
using HelpPoint.Infrastructure.Dtos.Response;
using HelpPoint.Infrastructure.Models.Ticket;

namespace HelpPoint.Features.Tickets;

public class TicketService(IMapper mapper, ITicketRepository repository, ICurrentUserAccessor currentUserAccessor, ISupportRequestRepository supportRequestRepository) : ITicket
{
    private const int ESTADO_SOLICITUD_APROBADA = 2;

    public async Task<TicketResponse> CreateTicket(TicketRequest request)
    {
        var createdByUserId = Guid.Parse(currentUserAccessor.GetCurrentUserId());

        if (request.SupportRequestId is { } srId)
        {
            var supportRequest = await supportRequestRepository
                                     .GetByIdAsync(srId)
                                 ?? throw new NotFoundException($"Solicitud de soporte {srId} no encontrada.");

            supportRequest.EstadoId = ESTADO_SOLICITUD_APROBADA;
            await supportRequestRepository.UpdateAsync(supportRequest);
        }

        var ticket = mapper.Map<Ticket>(request);
        ticket.Id              = Guid.CreateVersion7();
        ticket.CreatedByUserId = createdByUserId;
        ticket.FechaCreacion   = DateTime.UtcNow;
        ticket.FechaCierre     = null;

        await repository.AddAsync(ticket);

        return mapper.Map<TicketResponse>(ticket);
    }

    public async Task<TicketResponse> GetTicket(Guid id)
    {
        var ticketExistente = await repository.GetByIdAsync(id) ??
                              throw new NotFoundException("No se encontró el ticket");

        var estado = await repository.GetEstado(ticketExistente.EstadoId) ??
                     throw new NotFoundException("No se encontró el estado del ticket");

        var tipo = await repository.GetTipo(ticketExistente.TipoId) ??
                   throw new NotFoundException("No se encontró el tipo del ticket");

        var prioridad = await repository.GetPrioridad(ticketExistente.PrioridadId) ??
                        throw new NotFoundException("No se encontró la prioridad del ticket");

        var createdByUser = await repository.GetCreationUser(ticketExistente.CreatedByUserId) ??
                            throw new NotFoundException("No se encontró el usuario creador del ticket");

        var ticketResponse = new TicketResponse
        {
            Id = ticketExistente.Id,
            Titulo = ticketExistente.Titulo,
            OrdenEnTablero = ticketExistente.OrdenEnTablero,
            Descripcion = ticketExistente.Descripcion,
            Estado = estado,
            Tipo = tipo,
            Prioridad = prioridad,
            FechaCreacion = ticketExistente.FechaCreacion,
            FechaCierre = ticketExistente.FechaCierre,
            SupportRequestId = ticketExistente.SupportRequestId,
            CreatedBy = createdByUser
        };

        return ticketResponse;
    }

    public async Task<List<KanbanTicketResponse>> ListTicketsForKanban()
    {
        var result = await repository.ListTickets() ?? throw new NotFoundException("No se encontraron tickets");
        return result;
    }

    public async Task<TicketResponse> UpdateTicket(Guid id, TicketUpdateRequest request)
    {
        var ticket = await repository.GetByIdAsync(id)
                     ?? throw new NotFoundException("No se encontró el ticket");

        ticket.OrdenEnTablero = request.OrdenEnTablero;
        ticket.Titulo = request.Titulo;
        ticket.Descripcion = request.Descripcion;
        ticket.EstadoId = request.EstadoId;
        ticket.TipoId = request.TipoId;
        ticket.PrioridadId = request.PrioridadId;
        ticket.FechaCierre = request.FechaCierre;
        ticket.SupportRequestId = request.SupportRequestId;

        await repository.UpdateAsync(ticket);

        var estado = await repository.GetEstado(ticket.EstadoId)
                     ?? throw new NotFoundException("Estado no encontrado");
        var tipo = await repository.GetTipo(ticket.TipoId)
                   ?? throw new NotFoundException("Tipo no encontrado");
        var prioridad = await repository.GetPrioridad(ticket.PrioridadId)
                        ?? throw new NotFoundException("Prioridad no encontrada");
        var createdBy = await repository.GetCreationUser(ticket.CreatedByUserId)
                        ?? throw new NotFoundException("Usuario creador no encontrado");

        return new TicketResponse
        {
            Id = ticket.Id,
            Titulo = ticket.Titulo,
            OrdenEnTablero = ticket.OrdenEnTablero,
            Descripcion = ticket.Descripcion,
            Estado = estado,
            Tipo = tipo,
            Prioridad = prioridad,
            FechaCreacion = ticket.FechaCreacion,
            FechaCierre = ticket.FechaCierre,
            SupportRequestId = ticket.SupportRequestId,
            CreatedBy = createdBy
        };
    }

    public async Task<CommentResponse> AddCommentAsync(Guid ticketId, TicketCommentRequest request)
    {
        _ = await repository.GetByIdAsync(ticketId)
            ?? throw new NotFoundException("No se encontró el ticket");

        var userId = Guid.Parse(currentUserAccessor.GetCurrentUserId());
        var comment = new TicketComentario
        {
            Id = Guid.NewGuid(),
            TicketId = ticketId,
            UserId = userId,
            Comentario = request.Comentario,
            FechaCreacion = DateTime.UtcNow
        };

        await repository.AddCommentAsync(comment);

        var userLookup = await repository.GetCreationUser(userId)
                         ?? throw new NotFoundException("No se encontró el usuario");

        return new CommentResponse
        {
            Id = comment.Id,
            Comentario = comment.Comentario,
            FechaCreacion = comment.FechaCreacion,
            User = userLookup
        };
    }

    public async Task ReorderTicketsAsync(IEnumerable<ReorderTicketDto> reorderDto)
    {
        // 1) Materialize once
        var dtos = reorderDto.ToList();
        if (dtos.Count == 0)
        {
            throw new BadRequestCustomException("Se requiere al menos un ticket para reordenar.");
        }

        // 2) Extract IDs
        var ids = dtos.Select(d => Guid.Parse(d.TicketId)).ToList();

        // 3) Load all tickets in one go
        var tickets = await repository.GetManyByIdsAsync(ids)
                      ?? throw new NotFoundException("No se encontraron los tickets solicitados.");

        // 4) Apply updates
        foreach (var dto in dtos)
        {
            var ticket = tickets.FirstOrDefault(t => t.Id == Guid.Parse(dto.TicketId))
                         ?? throw new NotFoundException($"Ticket {dto.TicketId} no encontrado");

            ticket.EstadoId       = dto.EstadoId;
            ticket.OrdenEnTablero = dto.OrdenEnTablero;
            await repository.UpdateAsync(ticket);
        }
    }

    public async Task DeleteTicket(Guid id)
    {
        var ticket = await repository.GetByIdAsync(id) ?? throw new NotFoundException("Ticket no encontrado");

        await repository.DeleteAsync(ticket);
    }

    public async Task<bool> AssignUsers(string ticketId,AssignUsersRequest request) => await repository.AssignUsers(request.Users, ticketId);

    public async Task<List<UserProfileResponse>?> ListAssignedUsers(string id)
    {
        var listado = await repository.ListAssignedUsers(id);
        return listado;
    }
}
