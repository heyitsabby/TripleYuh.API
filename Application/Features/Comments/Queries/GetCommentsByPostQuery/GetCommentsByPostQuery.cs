using Application.Models.Comments;
using MediatR;

namespace Application.Features.Comments.Queries.GetCommentsByPostQuery
{
    public class GetCommentsByPostQuery : IRequest<IEnumerable<CommentResponse>>
    {
        public int PostId { get; set; }
    }
}
