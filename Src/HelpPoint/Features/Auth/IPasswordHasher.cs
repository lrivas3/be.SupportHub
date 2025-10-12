namespace HelpPoint.Features.Auth;

public interface IPasswordHasher
{
    public string HashPassword(string password);
    public bool Verify(string inputPassword, string passwordHash);
}
