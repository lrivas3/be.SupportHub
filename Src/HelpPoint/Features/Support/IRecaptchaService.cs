namespace HelpPoint.Features.Support;

public interface IRecaptchaService
{
  public Task<bool> ValidateAsync(string token);
}
