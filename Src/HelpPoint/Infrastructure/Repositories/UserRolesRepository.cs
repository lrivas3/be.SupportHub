using HelpPoint.Features.Users;
using HelpPoint.Infrastructure.DataBase;
using HelpPoint.Infrastructure.Models.Users;

namespace HelpPoint.Infrastructure.Repositories;

public class UserRolesRepository(HelpPointDbContext context) : IUserRolesRepository
{
    public async Task<UserRoles> AddAsync(UserRoles userRoles)
    {
        await context.UserRoles.AddAsync(userRoles);
        await context.SaveChangesAsync();
        return userRoles;
    }
}
