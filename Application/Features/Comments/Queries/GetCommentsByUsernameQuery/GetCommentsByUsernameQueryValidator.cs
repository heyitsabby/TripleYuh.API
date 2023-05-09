using FluentValidation;

namespace Application.Features.Comments.Queries.GetCommentsByUsernameQuery
{
    public class GetCommentsByUsernameQueryValidator : AbstractValidator<GetCommentsByUsernameQuery>
    {
        public GetCommentsByUsernameQueryValidator()
        {
            RuleFor(comment => comment.Username)
                .NotEmpty()
                .WithMessage("Username is required.");
        }
    }
}
