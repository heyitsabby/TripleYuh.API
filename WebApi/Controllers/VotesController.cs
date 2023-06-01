using Application.Common.Security;
using Application.Features.Votes.Commands.RemoveVoteOnCommentCommand;
using Application.Features.Votes.Commands.RemoveVoteOnPostCommand;
using Application.Features.Votes.Commands.VoteOnCommentCommand;
using Application.Features.Votes.Commands.VoteOnPostCommand;
using Application.Models.Votes;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    public class VotesController : ApiControllerBase
    {
        [HttpPost("/api/posts/{postId:int}/votes")]
        public async Task<ActionResult<VoteResponse>> VoteOnPostAsync(int postId, VoteOnPostCommand command)
        {
            command.PostId = postId;
            
            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpPost("/api/comments/{commentId:int}/votes")]
        public async Task<ActionResult<VoteResponse>> VoteOnCommentAsync(int commentId, VoteOnCommentCommand command)
        {
            command.CommentId = commentId;

            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("/api/comments/{commentId:int}/votes/{voteId:int}")]
        public async Task<ActionResult<VoteResponse>> RemoveVoteOnCommentAsync(int commentId, int voteId, RemoveVoteOnCommentCommand command)
        {
            command.CommentId = commentId;

            command.VoteId = voteId;

            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("/api/posts/{postId:int}/votes/{voteId:int}")]
        public async Task<ActionResult<VoteResponse>> RemoveVoteOnPostAsync(int postId, int voteId, RemoveVoteOnPostCommand command)
        {
            command.PostId = postId;

            command.VoteId = voteId;

            var response = await Mediator.Send(command);

            return Ok(response);
        }
    }
}
