using HelpPoint.Infrastructure.Dtos;
using HelpPoint.Infrastructure.Dtos.Request;
using HelpPoint.Infrastructure.Models.Users;

namespace HelpPoint.Features.Users;

public interface IUserService
{
    public Task<User?>               CreateUser(RegisterDto registerRequest);
    public Task<UserProfileResponse> GetUserProfile();
    public Task<List<UserProfileResponse?>?> ListUsers();
}
