using FluentValidation;

namespace Application.Features.Votes.Commands.RemoveVoteOnPostCommand
{
    public class RemoveVoteOnPostCommandValidator : AbstractValidator<RemoveVoteOnPostCommand>
    {
        public RemoveVoteOnPostCommandValidator()
        {
            RuleFor(vote => vote.VoteId)
                .NotEmpty()
                .WithMessage("Vote id is required.");

            RuleFor(vote => vote.PostId)
                .NotEmpty()
                .WithMessage("Post id is required.");

            RuleFor(vote => vote.Username)
                .NotEmpty()
                .WithMessage("Username is required.");
        }
    }
}
