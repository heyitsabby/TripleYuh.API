using FluentValidation;

namespace Application.Features.Comments.Queries
{
    public class GetCommentsByPostQueryValidator : AbstractValidator<GetCommentsByPostQuery>
    {
        public GetCommentsByPostQueryValidator()
        {
            RuleFor(comment => comment.PostId)
                .NotEmpty()
                .WithMessage("Post id is required.");
        }
    }
}
