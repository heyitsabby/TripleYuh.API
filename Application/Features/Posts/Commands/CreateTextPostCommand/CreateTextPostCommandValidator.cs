using Domain.Rules;
using FluentValidation;

namespace Application.Features.Posts.Commands.CreateTextPostCommand
{
    public class CreateTextPostCommandValidator : AbstractValidator<CreateTextPostCommand>
    {
        public CreateTextPostCommandValidator()
        {
            RuleFor(post => post.Body)
                .MaximumLength(PostRules.BodyMaximumLength)
                .When(post => !string.IsNullOrEmpty(post.Body))
                .WithMessage($"Body cannot exceed {PostRules.BodyMaximumLength} characters.");

            RuleFor(post => post.Title)
               .NotEmpty()
               .WithMessage("Title is required.")
               .MinimumLength(PostRules.TitleMinimumLength)
               .MaximumLength(PostRules.TitleMaximumLength)
               .WithMessage($"Title must be {PostRules.TitleMinimumLength} - {PostRules.TitleMaximumLength} characters long.");

            RuleFor(post => post.Username)
                .NotEmpty()
                .WithMessage("Username is required.");
        }
    }
}
