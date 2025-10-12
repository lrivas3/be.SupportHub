using HelpPoint.Common.Errors.Exceptions;
using HelpPoint.Infrastructure.Dtos.Request;
using HelpPoint.Infrastructure.Dtos.Response;
using System.Security.Claims;
using HelpPoint.Common.Constants;

namespace HelpPoint.Features.Auth;

public class AuthService(LoginValidator loginValidator,
    IPasswordHasher passwordHasher,
    ITokenGenerator tokenGenerator,
    IUserRepository userRepository
    ) : IAuth
{
    public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
    {
        var validationResult = await loginValidator.ValidateAsync(loginRequest);

        if (!validationResult.IsValid)
        {
            throw new HelpPointValidationException(validationResult);
        }

        var user = await userRepository.GetUserByEmailAsync(loginRequest.Email) ??
                   throw new NotFoundException("User not found");

        if (!passwordHasher.Verify(loginRequest.Password, user.PasswordHash))
        {
            throw new UnauthorizedException("Invalid email or password");
        }
        var roles = await userRepository.GetRolesByIdAsync(user.Id) ?? throw new NotFoundException(AppConstants.ErrorMessages.RolesNotFoundMsg);

        if (roles.Count == 0)
        {
            throw new NotFoundException(AppConstants.ErrorMessages.RolesNotFoundMsg);
        }

        var rolesList = roles.Select(x => x.NormalizedName).ToList() ??
                        throw new UnauthorizedException(AppConstants.ErrorMessages.RolesNotFoundMsg);

        var token        = tokenGenerator.GenerateToken(user.Id, user.UserName, rolesList!);
        var refreshToken = tokenGenerator.GenerateToken(user.Id, user.UserName, rolesList!, true);

        var response = new LoginResponse
        {
            UserName = user.UserName,
            Email = user.Email,
            Token = token,
            RefreshToken = refreshToken
        };

        return response;
    }

    public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
    {
        var principal = tokenGenerator.ValidateToken(refreshTokenRequest.RefreshToken) ??
                        throw new UnauthorizedException("Invalid refresh token");

        var userName = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(userName))
        {
            throw new UnauthorizedException("Invalid refresh token");
        }

        var user = await userRepository.GetUserByUserNameAsync(userName) ??
                   throw new NotFoundException("User not found");

        var roles = await userRepository.GetRolesByIdAsync(user.Id) ??
                    throw new NotFoundException("Roles for user not found");

        if (roles.Count == 0)
        {
            throw new NotFoundException("Roles for user not found");
        }

        var rolesList = roles.Select(x => x.NormalizedName).ToList();

        var newAccessToken = tokenGenerator.GenerateToken(user.Id,user.UserName, rolesList!);
        var newRefreshToken = tokenGenerator.GenerateToken(user.Id, user.UserName, rolesList!, true);

        return new LoginResponse
        {
            UserName = user.UserName,
            Email = user.Email,
            Token = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}
