using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpPoint.Infrastructure.Models.Ticket;

[Table("Tipo", Schema = "Ticket")]
public class Tipo
{
    [Key]
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
}
