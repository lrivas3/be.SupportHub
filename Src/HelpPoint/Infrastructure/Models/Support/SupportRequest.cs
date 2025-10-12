using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpPoint.Infrastructure.Models.Support;
[Table("SupportRequests", Schema = "Support")]
public class SupportRequest
{
    [Key]
    [MaxLength(36)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Titulo { get; set; } = string.Empty;

    [Required]
    public string Descripcion { get; set; } = string.Empty;
    [Required]
    public int EstadoId { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public DateTime? FechaResolucion { get; set; }

    public Guid? EmpleadoId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    public string? TokenVerificacion { get; set; }

    [ForeignKey("EmpleadoId")]
    public Empleado? Empleado { get; set; }
    [ForeignKey("EstadoId")]
    public SupportEstado Estado { get; set; } = null!;
}
