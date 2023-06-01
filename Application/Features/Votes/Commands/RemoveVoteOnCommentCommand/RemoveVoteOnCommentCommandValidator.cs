using FluentValidation;

namespace Application.Features.Votes.Commands.RemoveVoteOnCommentCommand
{
    public class RemoveVoteOnCommentCommandValidator : AbstractValidator<RemoveVoteOnCommentCommand>
    {
        public RemoveVoteOnCommentCommandValidator()
        {
            RuleFor(vote => vote.VoteId)
                .NotEmpty()
                .WithMessage("Vote id is required.");

            RuleFor(vote => vote.CommentId)
                .NotEmpty()
                .WithMessage("Comment id is required.");

            RuleFor(vote => vote.Username)
                .NotEmpty()
                .WithMessage("Username is required.");
        }
    }
}
