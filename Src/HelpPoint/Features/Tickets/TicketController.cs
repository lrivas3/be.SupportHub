using Asp.Versioning;
using HelpPoint.Infrastructure.Dtos.Request;
using Microsoft.AspNetCore.Mvc;

namespace HelpPoint.Features.Tickets;

[ApiController]
[Route("api/v{version:apiVersion}/tickets")]
[ApiVersion("1.0")]
public class TicketController(ITicket ticket) : ControllerBase
{
    [HttpPost]
    // [Authorize]
    public async Task<IActionResult> CreateTicket([FromBody] TicketRequest request)
    {
        var result = await ticket.CreateTicket(request);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetTickets()
    {
        var result = await ticket.ListTicketsForKanban();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTicket(Guid id)
    {
        var result = await ticket.GetTicket(id);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTicket(Guid id)
    {
        await ticket.DeleteTicket(id);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTicket(Guid id, [FromBody] TicketUpdateRequest request)
    {
        var updated = await ticket.UpdateTicket(id, request);
        return Ok(updated);
    }

    [HttpPost("{id:guid}/comments")]
    public async Task<IActionResult> AddTicketComment(Guid id, [FromBody] TicketCommentRequest request)
    {
        var comment = await ticket.AddCommentAsync(id, request);
        // 201 Created with the new comment in the body
        return CreatedAtAction(
            nameof(AddTicketComment),
            new { id, commentId = comment.Id },
            comment
        );
    }

    [HttpPut("reorder")]
    public async Task<IActionResult> ReorderTickets([FromBody] ReorderPayload payload)
    {
        if (payload.Tickets.Count == 0)
        {
            return BadRequest("Se requiere al menos un ticket para reordenar.");
        }

        await ticket.ReorderTicketsAsync(payload.Tickets);
        return NoContent();
    }

    [HttpPost("{id}/assigned")]
    public async Task<IActionResult> AssignUsersAsync(string id, [FromBody] AssignUsersRequest request)
    {
        var result = await ticket.AssignUsers(id, request);
        return Ok(result);
    }

    [HttpGet("{id}/assigned")]
    public async Task<IActionResult> GetAssignedToTicket(string id)
    {
        var result = await ticket.ListAssignedUsers(id);
        return Ok(result);
    }
}
