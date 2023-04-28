using FluentValidation;

namespace Application.Features.Accounts.Queries.GetAccountByUsername
{
    public class GetAccountByUsernameValidator : AbstractValidator<GetAccountByUsernameQuery>
    {
        public GetAccountByUsernameValidator() 
        {
            RuleFor(account => account.Username)
                .NotEmpty()
                .WithMessage("Username is required.");
        }
    }
}
