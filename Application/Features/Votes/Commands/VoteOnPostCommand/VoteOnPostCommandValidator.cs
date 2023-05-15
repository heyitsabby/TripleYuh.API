using Domain.Rules;
using FluentValidation;

namespace Application.Features.Votes.Commands.VoteOnPostCommand
{
    public class VoteOnPostCommandValidator : AbstractValidator<VoteOnPostCommand>
    {
        public VoteOnPostCommandValidator()
        {
            RuleFor(vote => vote.PostId)
                .NotEmpty()
                .WithMessage("Post id is required.");

            RuleFor(vote => vote.Username)
                .NotEmpty()
                .WithMessage("Username is required.");

            RuleFor(vote => vote.Value)
                .NotEmpty()
                .WithMessage("Value is required.")
                .Must(ValidValue)
                .WithMessage($"Value must be {VoteRules.Upvote} or {VoteRules.Downvote}");
        }

        private bool ValidValue(int value)
        {
            return value == VoteRules.Upvote || value == VoteRules.Downvote;
        }
    }
}
