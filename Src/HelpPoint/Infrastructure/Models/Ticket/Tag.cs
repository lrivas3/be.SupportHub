using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpPoint.Infrastructure.Models.Ticket;

[Table("Tags", Schema = "Ticket")]
public class Tag
{
    [Key]
    [MaxLength(36)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Nombre { get; set; } = string.Empty;
}
