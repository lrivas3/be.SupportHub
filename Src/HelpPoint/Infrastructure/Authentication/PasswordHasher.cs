using HelpPoint.Features.Auth;
using BC = BCrypt.Net.BCrypt;
namespace HelpPoint.Infrastructure.Authentication;


public class PasswordHasher : IPasswordHasher
{
    private const int WORK_FACTOR = 13;
    public string HashPassword(string password) =>
        BC.EnhancedHashPassword(password, WORK_FACTOR);

    public bool Verify(string inputPassword, string passwordHash) =>
        BC.EnhancedVerify(inputPassword, passwordHash);
}
