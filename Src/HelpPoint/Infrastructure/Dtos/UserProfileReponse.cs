namespace HelpPoint.Infrastructure.Dtos;

public class UserProfileResponse
{
    public required string Id { get; set; }
    public required string UserName { get; set; }
    public required string Name { get; set; }
    public string? LastName { get; set; }
    public string? Role { get; set; }

    public required string Email { get; set; }
    public required string Avatar { get; set; }
}
