using FluentValidation;

namespace Application.Features.Accounts.Commands.RefreshTokenCommand
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(token => token.RefreshToken)
                .NotEmpty()
                .WithMessage("Token is required.");
        }
    }
}
