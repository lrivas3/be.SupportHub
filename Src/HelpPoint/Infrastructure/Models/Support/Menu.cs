using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpPoint.Infrastructure.Models.Support;

[Table("Menus", Schema = "Support")]
public class Menu
{
    [Key]
    [MaxLength(36)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Url { get; set; }

    [MaxLength(100)]
    public string? Icon { get; set; }

    [MaxLength(36)]
    public Guid? ParentId { get; set; }

    [Required]
    public int OrderIndex { get; set; } = 0;

    [Required]
    public bool IsActive { get; set; } = true;

    [ForeignKey("ParentId")]
    public Menu? Parent { get; set; }

    public ICollection<Menu>? SubMenus { get; set; }
}
