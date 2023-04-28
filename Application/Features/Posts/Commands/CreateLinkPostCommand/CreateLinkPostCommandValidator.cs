using Domain.Rules;
using FluentValidation;

namespace Application.Features.Posts.Commands.CreateLinkPostCommand
{
    public class CreateLinkPostCommandValidator : AbstractValidator<CreateLinkPostCommand>
    {
        public CreateLinkPostCommandValidator()
        {
            RuleFor(post => post.Url)
                .NotEmpty()
                .WithMessage("URL is required.")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("Not a valid URL.");

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
