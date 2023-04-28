using FluentValidation;

namespace Application.Features.Accounts.Commands.UpdateAccountCommand
{
    public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            RuleFor(account => account.Username)
                .NotEmpty()
                .WithMessage("Username is required.");

            RuleFor(account => account.Email)
                .EmailAddress()
                .When(account => !string.IsNullOrEmpty(account.Email))
                .WithMessage("Invalid email.");
        }
    }
}
