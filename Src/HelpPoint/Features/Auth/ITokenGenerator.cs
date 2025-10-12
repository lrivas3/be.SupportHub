using System.Security.Claims;

namespace HelpPoint.Features.Auth;

public interface ITokenGenerator
{
    public string GenerateToken(Guid userId, string userName, List<string?> roles, bool isRefreshToken = false);
    public ClaimsPrincipal? ValidateToken(string token);
}
