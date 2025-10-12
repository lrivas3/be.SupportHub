using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpPoint.Infrastructure.Models.Ticket;
[Table("TicketPrioridades", Schema = "Ticket")]
public class TicketPrioridad
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(10)]
    public string Codigo { get; set; } = string.Empty;
}
