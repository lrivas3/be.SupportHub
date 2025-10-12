using FluentValidation;
using HelpPoint.Infrastructure.Dtos.Request;

namespace HelpPoint.Features.Auth;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty();

        RuleFor(x => x.Email).NotEmpty();
    }
}
