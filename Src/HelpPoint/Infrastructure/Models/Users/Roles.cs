using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpPoint.Infrastructure.Models.Users;

[Table("Roles", Schema = "Users")]
public class Roles
{
    [Key]
    public Guid Id { get; set; }
    [StringLength(25)]
    public string Name { get; set; } = null!;
    [StringLength(25)]
    public string NormalizedName { get; set; } = null!;
}
