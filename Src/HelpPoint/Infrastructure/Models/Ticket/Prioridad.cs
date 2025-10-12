using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpPoint.Infrastructure.Models.Ticket;

[Table("Prioridad", Schema = "Ticket")]
public class Prioridad
{
    [Key]
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
}
