using MediatR;

namespace Application.Features.Comments.DeleteCommentCommand
{
    public class DeleteCommentCommand : IRequest
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;
    }
}
