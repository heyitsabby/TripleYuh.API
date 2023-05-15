using Application.Common.Security;
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
    }
}
