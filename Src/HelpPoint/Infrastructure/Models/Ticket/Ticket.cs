using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HelpPoint.Infrastructure.Models.Support;

namespace HelpPoint.Infrastructure.Models.Ticket;

[Table("Tickets", Schema = "Ticket")]
public class Ticket
{
    [Key]
    public Guid Id { get; set; }

    public int? OrdenEnTablero { get; set; }

    [Required]
    [MaxLength(255)]
    public string Titulo { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    [Required]
    public int EstadoId { get; set; }
    [Required]
    public int TipoId { get; set; }
    [Required]
    public int PrioridadId { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public DateTime? FechaCierre { get; set; }

    public Guid? SupportRequestId { get; set; }

    [Required]
    [MaxLength(36)]
    public Guid CreatedByUserId { get; set; }

    [ForeignKey("EstadoId")]
    public Estado Estado { get; set; } = null!;

    [ForeignKey("TipoId")]
    public Tipo Tipo { get; set; } = null!;
    [ForeignKey("PrioridadId")]
    public Prioridad Prioridad { get; set; } = null!;

    [ForeignKey("SupportRequestId")]
    public SupportRequest? SupportRequest { get; set; }
}
