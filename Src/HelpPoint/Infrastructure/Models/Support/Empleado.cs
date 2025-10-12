using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpPoint.Infrastructure.Models.Support;

[Table("Empleados", Schema = "Support")]
public class Empleado
{
    [Key] [MaxLength(36)] public required Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public bool Estado { get; set; }

    [Required]
    public Guid UnidadId { get; set; }

    [ForeignKey("UnidadId")]
    public Unidad Unidad { get; set; } = null!;
}
