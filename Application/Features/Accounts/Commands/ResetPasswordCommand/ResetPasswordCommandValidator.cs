using FluentValidation;

namespace Application.Features.Accounts.Commands.ResetPasswordCommand
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(account => account.Token)
                .NotEmpty()
                .WithMessage("Token is required.");

            RuleFor(account => account.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .Must((a, b) => a.Password == a.ConfirmPassword)
                .WithMessage("Password and confirm password must match.");

            RuleFor(account => account.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm password is required.");
        }
    }
}
