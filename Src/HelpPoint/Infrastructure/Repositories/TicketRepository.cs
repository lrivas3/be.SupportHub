using HelpPoint.Features.Common;
using HelpPoint.Features.Tickets;
using HelpPoint.Infrastructure.DataBase;
using HelpPoint.Infrastructure.Dtos;
using HelpPoint.Infrastructure.Dtos.Response;
using HelpPoint.Infrastructure.Models.Ticket;
using Microsoft.EntityFrameworkCore;

namespace HelpPoint.Infrastructure.Repositories;

public class TicketRepository(HelpPointDbContext context) : Repository<Ticket>(context), ITicketRepository
{
    public async Task<LookUpResponse?> GetEstado(int id) =>
        await context.TicketEstados.Where(x => x.Id == id)
            .Select(x => new LookUpResponse { Id = x.Id, Nombre = x.NombreEstado, })
            .FirstOrDefaultAsync();

    public async Task<LookUpResponse?> GetTipo(int id) =>
        await context.Tipos.Where(x => x.Id == id)
            .Select(x => new LookUpResponse { Id = x.Id, Nombre = x.Nombre, })
            .FirstOrDefaultAsync();

    public async Task<LookUpResponse?> GetPrioridad(int id) =>
        await context.Prioridades.Where(x => x.Id == id)
            .Select(x => new LookUpResponse { Id = x.Id, Nombre = x.Nombre, })
            .FirstOrDefaultAsync();

    public async Task<UserLookUpResponse?> GetCreationUser(Guid id) =>
        await context.Users.Where(x => x.Id == id)
            .Select(x => new UserLookUpResponse { CreatedByUserId = x.Id, CreatedByUserName = x.Name })
            .FirstOrDefaultAsync();

    public async Task<List<KanbanTicketResponse>?> ListTickets() => await context.Tickets.Select(x =>
        new KanbanTicketResponse
        {
            Id = x.Id.ToString(),
            Title = x.Titulo,
            Description = x.Descripcion,
            Estado = new LookUpResponse { Id = x.EstadoId, Nombre = x.Estado.NombreEstado },
            Tipo = new LookUpResponse { Id = x.TipoId, Nombre = x.Tipo.Nombre },
            Prioridad = new LookUpResponse { Id = x.PrioridadId, Nombre = x.Prioridad.Nombre },
            CreationDate = x.FechaCreacion,
            ClosureDate = x.FechaCierre,
            OrderInBoard = x.OrdenEnTablero ?? 0,
            Tags = context.TicketTags.Where(j => j.TicketId == x.Id)
                .Select(tt => tt.Tag.Nombre)
                .ToList(),
            Progress = null,
            Checklist = null,
            Attachments = null,
            Avatars = new List<string>(),
            SupportRequestId = x.SupportRequestId ?? null,
            CreatedBy = context.Users.Where(u => u.Id == x.CreatedByUserId)
                .Select(u => new UserLookUpResponse { CreatedByUserId = u.Id, CreatedByUserName = u.Name })
                .FirstOrDefault()!,
            Comments = context.TicketComments.Where(c => c.TicketId == x.Id).Select(c => new CommentResponse
            {
                Id = c.Id,
                User = context.Users.Where(u => u.Id == c.UserId)
                        .Select(u => new UserLookUpResponse { CreatedByUserId = u.Id, CreatedByUserName = u.Name })
                        .FirstOrDefault()!,
                Comentario = c.Comentario,
                FechaCreacion = c.FechaCreacion
            })
                .OrderByDescending(c => c.FechaCreacion)
                .ToList()
        }).ToListAsync();


    public async Task AddCommentAsync(TicketComentario comment)
    {
        _ = context.TicketComments.Add(comment);
        _ = await context.SaveChangesAsync();
    }

    public async Task<List<TicketComentario>> ListCommentsByTicketIdAsync(Guid ticketId) =>
        await context.TicketComments
            .Where(c => c.TicketId == ticketId)
            .ToListAsync();

    public async Task<List<Ticket>?> GetManyByIdsAsync(IEnumerable<Guid> ids) =>
        await context.Tickets
            .Where(t => ids.Contains(t.Id))
            .ToListAsync();

    public async Task<bool> AssignUsers(List<string> requestUsers, string requestTicketId)
    {
        //quitar a todos
        var ticketUsers = context.TicketAsignaciones
            .Where(x => x.TicketId == Guid.Parse(requestTicketId)).ToList();
        context.TicketAsignaciones.RemoveRange(ticketUsers);

        // asignar lista
        var asignaciones = requestUsers.Select(requestUser => new TicketAsignacion
        {
            Id = Guid.CreateVersion7(),
            TicketId = Guid.Parse(requestTicketId),
            UserId = Guid.Parse(requestUser),
            FechaAsignacion = DateTime.UtcNow,
            FechaFin = null,
            TiempoEmpleadoMinutos = 0
        })
            .ToList();
        await context.TicketAsignaciones.AddRangeAsync(asignaciones);
        return await context.SaveChangesAsync() == requestUsers.Count;
    }

    public async Task<List<UserProfileResponse>?> ListAssignedUsers(string id) =>
        await context.TicketAsignaciones
            .Where(ticketAsign => ticketAsign.TicketId == Guid.Parse(id))
            .Join(
                context.Users,
                ticketAsign => ticketAsign.UserId,
                user => user.Id,
                (ticketAsign, user) => new { ticketAsign, user }
            )
            .GroupJoin(
                context.UserRoles.Join(context.Roles, ur => ur.RoleId, r => r.Id,
                    (ur, r) => new { ur.UserId, RoleName = r.Name }),
                temp => temp.user.Id,
                ur => ur.UserId,
                (temp, roles) => new
                {
                    temp.ticketAsign,
                    temp.user,
                    Role = roles.Select(r => r.RoleName).FirstOrDefault()
                }
            )
            .Select(result => new UserProfileResponse
            {
                Id = result.user.Id.ToString(),
                UserName = result.user.UserName,
                Name = result.user.Name,
                LastName = result.user.LastName,
                Role = result.Role,
                Email = result.user.Email,
                Avatar = Utils.CreateAvatarLetters(result.user.Name, result.user.LastName)
            })
            .Distinct()
            .ToListAsync();
}
