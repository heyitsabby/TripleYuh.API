using FluentValidation;

namespace Application.Features.Accounts.Commands.AuthenticateCommand
{
    public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator()
        {
            RuleFor(account => account.Email)
                .NotEmpty()
                .WithMessage("Email is required.");

            RuleFor(account => account.Password)
                .NotEmpty()
                .WithMessage("Password is required");
        }
    }
}
