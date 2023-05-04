using Application.Models.Comments;
using MediatR;

namespace Application.Features.Comments.Commands.CreateCommentCommand
{
    public class CreateCommentCommand : IRequest<CommentResponse>
    {
        public int PostId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public int? ParentId { get; set; }


    }
}
