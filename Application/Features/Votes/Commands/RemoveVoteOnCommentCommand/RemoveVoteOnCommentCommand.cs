using Application.Models.Votes;
using MediatR;

namespace Application.Features.Votes.Commands.RemoveVoteOnCommentCommand
{
    public class RemoveVoteOnCommentCommand : IRequest<VoteResponse>
    {
        public int CommentId { get; set; }


        public int VoteId { get; set; }

        public string Username { get; set; } = string.Empty;
    }
}
