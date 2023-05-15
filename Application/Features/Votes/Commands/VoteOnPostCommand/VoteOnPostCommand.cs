using Application.Models.Votes;
using MediatR;

namespace Application.Features.Votes.Commands.VoteOnPostCommand
{
    public class VoteOnPostCommand : IRequest<VoteResponse>
    {
        public int PostId { get; set; }

        public int Value { get; set; }

        public string Username { get; set; } = string.Empty;
    }
}
