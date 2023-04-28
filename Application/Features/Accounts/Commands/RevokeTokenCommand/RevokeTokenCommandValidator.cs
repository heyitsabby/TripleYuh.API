using FluentValidation;

namespace Application.Features.Accounts.Commands.RevokeTokenCommand
{
    public class RevokeTokenCommandValidator : AbstractValidator<RevokeTokenCommand>
    {
        public RevokeTokenCommandValidator()
        {
            RuleFor(account => account.Token)
                .NotEmpty()
                .WithMessage("Token is required.");
        }
    }
}
