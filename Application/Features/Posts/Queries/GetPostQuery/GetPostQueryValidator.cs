using FluentValidation;

namespace Application.Features.Posts.Queries.GetPostQuery
{
    public class GetPostQueryValidator : AbstractValidator<GetPostQuery>
    {
        public GetPostQueryValidator()
        {
            RuleFor(post => post.Id)
                .NotEmpty()
                .WithMessage("Id is required.");
        }
    }
}
