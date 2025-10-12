using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpPoint.Infrastructure.Models.Ticket;

[Table("TicketHistorial", Schema = "Ticket")]
public class TicketHistorial
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid TicketId { get; set; }

    [Required]
    public DateTime FechaCambio { get; set; } = DateTime.UtcNow;

    public string? CampoModificado { get; set; }

    public string? ValorAnterior { get; set; }

    public string? ValorNuevo { get; set; }

    public Guid? ChangedByUserId { get; set; }
}
