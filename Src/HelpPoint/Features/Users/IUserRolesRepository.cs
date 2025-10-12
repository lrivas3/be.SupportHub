using HelpPoint.Infrastructure.Models.Users;

namespace HelpPoint.Features.Users;

public interface IUserRolesRepository
{
    public Task<UserRoles> AddAsync(UserRoles userRoles);
}
