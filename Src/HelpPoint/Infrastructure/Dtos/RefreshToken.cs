namespace HelpPoint.Infrastructure.Dtos;

public class RefreshToken
{
    public required string Token { get; set; }
    public DateTime TokenCreatedAt { get; set; }
    public DateTime TokenExpireddAt { get; set; }
}
