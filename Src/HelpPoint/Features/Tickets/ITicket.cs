using HelpPoint.Infrastructure.Dtos;
using HelpPoint.Infrastructure.Dtos.Request;
using HelpPoint.Infrastructure.Dtos.Response;
using Microsoft.AspNetCore.Mvc;

namespace HelpPoint.Features.Tickets;

public interface ITicket
{
    public Task<TicketResponse>             CreateTicket(TicketRequest request);
    public Task<TicketResponse>             GetTicket(Guid id);
    public Task<List<KanbanTicketResponse>> ListTicketsForKanban();
    public Task<TicketResponse>             UpdateTicket(Guid id, TicketUpdateRequest request);
    public Task<CommentResponse>            AddCommentAsync(Guid ticketId, TicketCommentRequest request);
    public Task                             ReorderTicketsAsync(IEnumerable<ReorderTicketDto> reorderDto);
    public Task                             DeleteTicket(Guid id);
    public Task<bool>                       AssignUsers(string ticketId,AssignUsersRequest request);
    public Task<List<UserProfileResponse>?> ListAssignedUsers(string id);
}
