using Application.Common.Security;
using Application.Features.Comments.Commands.CreateCommentCommand;
using Application.Features.Comments.Commands.DeleteCommentCommand;
using Application.Features.Comments.Commands.UpdateCommentCommand;
using Application.Features.Comments.Queries;
using Application.Models.Comments;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    public class CommentsController : ApiControllerBase
    {
        [HttpPost("/api/posts/{postId:int}/comments")]
        public async Task<ActionResult<CommentResponse>> CreateAsync(int postId, CreateCommentCommand command)
        {
            command.PostId = postId;

            if (command.Username != Account?.Username && Account?.Role != Domain.Entities.Role.Admin)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("{commentId:int}")]
        public async Task<IActionResult> DeleteAsync(int commentId, DeleteCommentCommand command)
        {
            if (Account?.Username != command.Username && Account?.Role != Domain.Entities.Role.Admin) 
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            if (commentId != command.CommentId)
            {
                return BadRequest();
            }

            command.Username = Account.Username;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{commentId:int}")]
        public async Task<ActionResult<CommentResponse>> UpdateAsync(int commentId, UpdateCommentCommand command)
        {
            if (commentId != command.Id)
            {
                return BadRequest();
            }

            if (Account?.Username != command.Username && Account?.Role != Domain.Entities.Role.Admin)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("/api/posts/{postId}/comments")]
        public async Task<ActionResult<IEnumerable<CommentResponse>>> GetAllByPostAsync(int postId)
        {
            var query = new GetCommentsByPostQuery { PostId = postId };

            var response = await Mediator.Send(query);

            return Ok(response);
        }
    }
}
