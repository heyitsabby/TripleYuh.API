﻿using Application.Common.Security;
using Application.Features.Comments;
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
    }
}