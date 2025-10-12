namespace HelpPoint.Features.Auth;

public interface ICurrentUserAccessor
{
    public string  GetCurrentUsername();
    public string  GetCurrentUserId();
    public string  GetCurrentUserSub();
    public string  GetCurrentRole();
    public string? GetRawBearerToken();
}
