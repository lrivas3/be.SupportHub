using HelpPoint.Infrastructure.Dtos.Email;

namespace HelpPoint.Features.Emails;

public interface IEmailService
{
    public Task<bool> SendEmailAsync(EmailDto request);
}

