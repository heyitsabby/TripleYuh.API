using Application.Common.Interfaces;
using Application.Models.Votes;
using MediatR;

namespace Application.Features.Votes.Commands.RemoveVoteOnPostCommand
{
    public class RemoveVoteOnPostCommandHandler : IRequestHandler<RemoveVoteOnPostCommand, VoteResponse>
    {
        private readonly IVoteService voteService;

        public RemoveVoteOnPostCommandHandler(IVoteService voteService)
        {
            this.voteService = voteService;
        }

        public async Task<VoteResponse> Handle(RemoveVoteOnPostCommand request, CancellationToken cancellationToken)
        {
            var response = await voteService.RemovePostVoteAsync(request.PostId, request.VoteId, request.Username);

            return response;
        }
    }
}
