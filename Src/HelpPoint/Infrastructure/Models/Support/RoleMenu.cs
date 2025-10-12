using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpPoint.Infrastructure.Models.Support;

[Table("RoleMenus", Schema = "Support")]
public class RoleMenu
{
    [Key]
    public Guid RoleId { get; set; }

    [Key]
    public Guid MenuId { get; set; }
    [ForeignKey("MenuId")]
    public Menu Menu { get; set; } = null!;
}
