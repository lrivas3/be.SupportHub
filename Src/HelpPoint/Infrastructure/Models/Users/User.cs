using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpPoint.Infrastructure.Models.Users;

[Table("Users", Schema = "Users")]
public class User
{
    [Key]
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public DateTime LockOutEnd { get; set; }
    public bool LockOutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
    public string PasswordHash { get; set; } = null!;
    public Guid? ManagerId { get; set; }
}
