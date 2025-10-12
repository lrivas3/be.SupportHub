using System.ComponentModel.DataAnnotations;

namespace HelpPoint.Infrastructure.Dtos.Request;

public class RegisterDto
{
    [Required]
    public required string UserName { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; set; }
    [Required(ErrorMessage = "LastName is required")]
    public required string LastName { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; set; }
    public required string PhoneNumber { get; set; }
}
