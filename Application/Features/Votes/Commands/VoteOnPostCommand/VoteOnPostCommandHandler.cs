using Application.Common.Interfaces;
using Application.Models.Votes;
using MediatR;

namespace Application.Features.Votes.Commands.VoteOnPostCommand
{
    public class VoteOnPostCommandHandler : IRequestHandler<VoteOnPostCommand, VoteResponse>
    {
        private readonly IVoteService voteService;

        public VoteOnPostCommandHandler(IVoteService voteService)
        {
            this.voteService = voteService;
        }

        public async Task<VoteResponse> Handle(VoteOnPostCommand request, CancellationToken cancellationToken)
        {
            var response = await voteService.VoteOnPostAsync(request.Value, request.PostId, request.Username);

            return response;
        }
    }
}
