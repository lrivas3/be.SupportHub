using System.ComponentModel.DataAnnotations;

namespace HelpPoint.Infrastructure.Dtos.Request;

public class TicketCommentRequest
{
    [Required] public string Comentario { get; set; } = string.Empty;
}
