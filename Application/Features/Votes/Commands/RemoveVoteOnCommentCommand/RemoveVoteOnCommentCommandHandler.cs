using Application.Common.Interfaces;
using Application.Models.Votes;
using MediatR;

namespace Application.Features.Votes.Commands.RemoveVoteOnCommentCommand
{
    public class RemoveVoteOnCommentCommandHandler : IRequestHandler<RemoveVoteOnCommentCommand, VoteResponse>
    {
        private readonly IVoteService voteService;

        public RemoveVoteOnCommentCommandHandler(IVoteService voteService)
        {
            this.voteService = voteService;
        }

        public async Task<VoteResponse> Handle(RemoveVoteOnCommentCommand request, CancellationToken cancellationToken)
        {
            var response = await voteService.RemoveCommentVoteAsync(request.CommentId, request.VoteId, request.Username);

            return response;
        }
    }
}
