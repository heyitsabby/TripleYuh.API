using FluentValidation;

namespace Application.Features.Accounts.Commands.ForgotPasswordCommand
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(account => account.Email)
                .NotEmpty()
                .WithMessage("Email is required.");
        }
    }
}
