using HelpPoint.Common;
using HelpPoint.Infrastructure.Dtos;
using HelpPoint.Infrastructure.Dtos.Response;
using HelpPoint.Infrastructure.Models.Ticket;

namespace HelpPoint.Features.Tickets;

public interface ITicketRepository : IRepository<Ticket>
{
    public Task<LookUpResponse?>             GetEstado(int id);
    public Task<LookUpResponse?>             GetTipo(int id);
    public Task<LookUpResponse?>             GetPrioridad(int id);
    public Task<UserLookUpResponse?>         GetCreationUser(Guid id);
    public Task<List<KanbanTicketResponse>?> ListTickets();
    public Task                              AddCommentAsync(TicketComentario comment);
    public Task<List<TicketComentario>>      ListCommentsByTicketIdAsync(Guid ticketId);
    public Task<List<Ticket>?>               GetManyByIdsAsync(IEnumerable<Guid> ids);
    public Task<bool>                        AssignUsers(List<string> requestUsers, string requestTicketId);
    public Task<List<UserProfileResponse>?>  ListAssignedUsers(string id);
}
