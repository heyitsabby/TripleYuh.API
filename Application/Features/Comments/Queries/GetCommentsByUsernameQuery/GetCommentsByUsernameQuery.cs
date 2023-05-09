using Application.Models.Comments;
using MediatR;

namespace Application.Features.Comments.Queries.GetCommentsByUsernameQuery
{
    public class GetCommentsByUsernameQuery : IRequest<IEnumerable<CommentResponse>>
    {
        public string Username { get; set; } = string.Empty;
    }
}
