using FluentValidation;

namespace Application.Features.Accounts.Commands.VerifyEmailCommand
{
    public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
    {
        public VerifyEmailCommandValidator()
        {
            RuleFor(account => account.Token)
                .NotEmpty()
                .WithMessage("Token is required.");
        }
    }
}
