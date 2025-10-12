namespace HelpPoint.Infrastructure.Dtos.Response;

public class LoginResponse
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
}
