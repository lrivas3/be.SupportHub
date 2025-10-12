using HelpPoint.Common.Errors.Exceptions;
using HelpPoint.Config;
using HelpPoint.Features.Auth;
using HelpPoint.Features.Common;
using HelpPoint.Infrastructure.Dtos;
using HelpPoint.Infrastructure.Dtos.Request;
using HelpPoint.Infrastructure.Models.Users;

namespace HelpPoint.Features.Users;

public class UserService(IUserRepository userRepository,
    IUserRolesRepository userRolesRepository,
    ICurrentUserAccessor currentUserAccessor,
    IPasswordHasher passwordHasher) : IUserService
{
    public async Task<User?> CreateUser(RegisterDto registerRequest)
    {
        var userExists = await userRepository.UserExists(registerRequest.UserName);
        var emailExists = await userRepository.EmailExists(registerRequest.UserName);
        if (userExists)
        {
            throw new UserConflictException("Usuario no disponible");
        }
        if (emailExists)
        {
            throw new UserConflictException("Email no disponible");
        }

        var newUser = new User
        {
            Id = Guid.CreateVersion7(),
            UserName = registerRequest.UserName,
            Name = registerRequest.Name,
            LastName = registerRequest.LastName,
            Email = registerRequest.Email,
            EmailConfirmed = false,
            PhoneNumber = registerRequest.PhoneNumber,
            LockOutEnd = DateTime.UtcNow,
            LockOutEnabled = false,
            AccessFailedCount = 0,
            PasswordHash = passwordHasher.HashPassword(registerRequest.Password),
            ManagerId = null
        };

        await userRepository.CustomAddAsync(newUser);

        var newUserRole = new UserRoles
        {
            UserId = newUser.Id,
            RoleId = AppConfigConstants.RolesConstants.SupportStaffId
        };
        await userRolesRepository.AddAsync(newUserRole);

        return newUser;
    }

    public async Task<UserProfileResponse> GetUserProfile()
    {
        var currentUser = currentUserAccessor.GetCurrentUsername();
        var user = await userRepository.GetUserByUserNameAsync(currentUser);
        if (user == null)
        {
            throw new NotFoundException("Usuario no encontrado: "+ currentUser);
        }

        var userRoles = await userRepository.GetRolesByIdAsync(user.Id);

        var response = new UserProfileResponse
        {
            Id = user.Id.ToString(),
            Role = userRoles.FirstOrDefault()?.NormalizedName,
            UserName = user.UserName,
            Name = user.Name + " " + user.LastName,
            Email = user.Email,
            Avatar = Utils.CreateAvatarLetters(user.Name, user.LastName)
        };

        return response;
    }

    public async Task<List<UserProfileResponse?>?> ListUsers()
    {
        var users = await userRepository.GetAllAsync();
        return [.. users.Select(user => new UserProfileResponse
            {
                Id       = user.Id.ToString(),
                UserName = user.UserName,
                Name     = user.Name,
                LastName = user.LastName,
                Email    = user.Email,
                Avatar   = Utils.CreateAvatarLetters(user.Name, user.LastName)
            })];
    }

}

