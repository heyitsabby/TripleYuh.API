using Application.Models.Comments;
using MediatR;

namespace Application.Features.Comments.UpdateCommentCommand
{
    public class UpdateCommentCommand : IRequest<CommentResponse>
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;
    }
}
