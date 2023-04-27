using Application.Common.Security;
using Application.Features.Posts.Commands.CreateLinkPostCommand;
using Application.Features.Posts.Commands.CreateTextPostCommand;
using Application.Features.Posts.Commands.DeletePostCommand;
using Application.Features.Posts.Commands.UpdatePostCommand;
using Application.Features.Posts.Queries.GetPostQuery;
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id, DeletePostCommand command)
        {
            if (command.Id != id)
            {
                return BadRequest(new { message = "Ids don't match." });
            }

            if (command.Username != Account?.Username && Account?.Role != Role.Admin)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PostResponse>> GetAsync(int id)
        {
            var query = new GetPostQuery { Id = id };

            var post = await Mediator.Send(query);

            return Ok(post);
        }
    }
}
