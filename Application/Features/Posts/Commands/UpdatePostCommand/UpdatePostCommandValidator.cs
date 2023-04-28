using Domain.Rules;
using FluentValidation;

namespace Application.Features.Posts.Commands.UpdatePostCommand
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(post => post.Id)
                .NotEmpty()
                .WithMessage("Id is required.");

            RuleFor(post => post.Username)
                .NotEmpty()
                .WithMessage("Username is required.");

            RuleFor(post => post.Body)
                .MaximumLength(PostRules.BodyMaximumLength)
                .When(post => !string.IsNullOrWhiteSpace(post.Body))
                .WithMessage($"Body cannot exceed {PostRules.BodyMaximumLength} characters.");

        }
    }
}
