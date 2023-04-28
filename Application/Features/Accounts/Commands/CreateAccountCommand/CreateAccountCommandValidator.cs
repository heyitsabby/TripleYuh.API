using FluentValidation;

namespace Application.Features.Accounts.Commands.CreateAccountCommand
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {

            RuleFor(account => account.Email)
                .NotEmpty()
                .WithMessage("Email is required.");


            RuleFor(account => account.Username)
                .NotEmpty()
                .WithMessage("Username is required.");


            RuleFor(account => account.Role)
                .NotEmpty()
                .WithMessage("Role is required.");

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
