using Application.Common.Security;
using Application.Features.Posts.Commands.CreateLinkPostCommand;
using Application.Features.Posts.Commands.CreateTextPostCommand;
using Application.Features.Posts.Commands.UpdatePostCommand;
using Application.Models.Accounts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    public class PostsController : ApiControllerBase
    {
        [HttpPost("link")]
        public async Task<ActionResult<PostResponse>> CreateLinkPostAsync(CreateLinkPostCommand command)
        {
            if (command.Username != Account?.Username && Account?.Role != Role.Admin)
            {
                return Unauthorized(new { message = "Unauthorized " });
            }

            var post = await Mediator.Send(command);

            return Ok(post);
        }

        [HttpPost("text")]
        public async Task<ActionResult<PostResponse>> CreateTextPostAsync(CreateTextPostCommand command)
        {
            if (command.Username != Account?.Username && Account?.Role != Role.Admin)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var post = await Mediator.Send(command);

            return Ok(post);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<PostResponse>> UpdateAsync(int id, UpdatePostCommand command)
        {
            if (command.Id != id)
            {
                return BadRequest(new { message = "Ids don't match." });
            }

            if (command.Username != Account?.Username && Account?.Role != Role.Admin)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var post = await Mediator.Send(command);

            return Ok(post);
        }
    }
}
