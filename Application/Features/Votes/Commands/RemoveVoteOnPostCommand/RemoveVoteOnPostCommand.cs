using Application.Models.Votes;
using MediatR;

namespace Application.Features.Votes.Commands.RemoveVoteOnPostCommand
{
    public class RemoveVoteOnPostCommand : IRequest<VoteResponse>
    {
        public int PostId { get; set; }

        public int VoteId  { get; set; }

        public string Username { get; set; } = string.Empty;
    }
}
