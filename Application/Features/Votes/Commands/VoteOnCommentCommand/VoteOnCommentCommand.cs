using Application.Models.Votes;
using MediatR;

namespace Application.Features.Votes.Commands.VoteOnCommentCommand
{
    public class VoteOnCommentCommand : IRequest<VoteResponse>
    {
        public int CommentId { get; set; }

        public int Value { get; set; }

        public string Username { get; set; } = string.Empty;
    }
}
