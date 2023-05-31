using Application.Common.Interfaces;
using Application.Models.Votes;
using MediatR;

namespace Application.Features.Votes.Commands.VoteOnCommentCommand
{
    public class VoteOnCommentCommandHandler : IRequestHandler<VoteOnCommentCommand, VoteResponse>
    {
        private readonly IVoteService voteService;

        public VoteOnCommentCommandHandler(IVoteService voteService)
        {
            this.voteService = voteService;
        }

        public async Task<VoteResponse> Handle(VoteOnCommentCommand request, CancellationToken cancellationToken)
        {
            var response = await voteService.VoteOnCommentAsync(request.Value, request.CommentId, request.Username);

            return response;
        }
    }
}
