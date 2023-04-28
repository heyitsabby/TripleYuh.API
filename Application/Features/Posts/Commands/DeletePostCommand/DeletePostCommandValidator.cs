using FluentValidation;

namespace Application.Features.Posts.Commands.DeletePostCommand
{
    public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(post => post.Id)
                .NotEmpty()
                .WithMessage("Id is required.");

            RuleFor(post => post.Username)
                .NotEmpty()
                .WithMessage("Username is required.");
        }
    }
}
