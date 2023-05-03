using MediatR;

namespace Application.Features.Comments.DeleteCommentCommand
{
    public class DeleteCommentCommand : IRequest
    {
        public int CommentId { get; set; }

        public string Username { get; set; } = string.Empty;
    }
}
