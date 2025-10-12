using System.ComponentModel.DataAnnotations.Schema;

namespace HelpPoint.Infrastructure.Models.Users;

[Table("UserRoles", Schema = "Users")]
public class UserRoles
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
