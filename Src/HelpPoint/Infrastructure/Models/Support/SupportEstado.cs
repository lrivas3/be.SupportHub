using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpPoint.Infrastructure.Models.Support;

[Table("SupportEstado", Schema = "Support")]
public class SupportEstado
{
    [Key]
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
}
