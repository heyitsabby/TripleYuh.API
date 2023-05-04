using FluentValidation;

namespace Application.Features.Comments.UpdateCommentCommand
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(comment => comment.Id)
                .NotEmpty()
                .WithMessage("Id is required.");

            RuleFor(comment => comment.Username)
                .NotEmpty()
                .WithMessage("Username is required");

            RuleFor(comment => comment.Body)
                .NotEmpty()
                .WithMessage("Body is required.");
        }
    }
}
