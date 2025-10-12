using HelpPoint.Infrastructure.Dtos.Request;
using HelpPoint.Infrastructure.Dtos.Response;

namespace HelpPoint.Features.Auth;

public interface IAuth
{
    public Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
    public Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
}
