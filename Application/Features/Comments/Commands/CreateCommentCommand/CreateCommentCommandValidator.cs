using Domain.Rules;
using FluentValidation;

namespace Application.Features.Comments.Commands.CreateCommentCommand
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(comment => comment.PostId)
                .NotEmpty()
                .WithMessage("Post id is required.");

            RuleFor(comment => comment.Username)
                .NotEmpty()
                .WithMessage("Username is required");

            RuleFor(comment => comment.Body)
                .NotEmpty()
                .WithMessage("Body is required")
                .MinimumLength(CommentRules.BodyMinimumLength)
                .MaximumLength(CommentRules.BodyMaximumLength)
                .WithMessage($"Body must be between {CommentRules.BodyMinimumLength} and {CommentRules.BodyMaximumLength} characters long.");
        }
    }
}
