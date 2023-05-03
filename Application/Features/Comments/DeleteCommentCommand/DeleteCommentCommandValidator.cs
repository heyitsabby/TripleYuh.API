using FluentValidation;

namespace Application.Features.Comments.DeleteCommentCommand
{
    public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
    {
        public DeleteCommentCommandValidator()
        {
            RuleFor(comment => comment.CommentId)
                .NotEmpty()
                .WithMessage("Comment id is required.");

            RuleFor(comment => comment.Username)
                .NotEmpty()
                .WithMessage("Username is required.");
        }
    }
}
