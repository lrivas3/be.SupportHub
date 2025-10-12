using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpPoint.Infrastructure.Models.Ticket;

[Table("Estado", Schema = "Ticket")]
public class Estado
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(15)]
    public string NombreEstado { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Descripcion { get; set; } = string.Empty;
}
