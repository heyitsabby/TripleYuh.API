using FluentValidation;

namespace Application.Features.Accounts.Commands.ValidateResetTokenCommand
{
    public class ValidateResetTokenValidator : AbstractValidator<ValidateResetTokenCommand>
    {
        public ValidateResetTokenValidator()
        {
            RuleFor(account => account.Token)
                .NotEmpty()
                .WithMessage("Token is required.");
        }
    }
}
