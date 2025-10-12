using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpPoint.Infrastructure.Models.Ticket;
[Table("TicketAsignaciones", Schema = "Ticket")]
public class TicketAsignacion
{
    [Key]
    [MaxLength(36)]
    public Guid Id { get; set; }

    [Required]
    public Guid TicketId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;

    public DateTime? FechaFin { get; set; }

    [Required]
    public int TiempoEmpleadoMinutos { get; set; } = 0;

    [ForeignKey("TicketId")]
    public Infrastructure.Models.Ticket.Ticket Ticket { get; set; } = null!;
}
