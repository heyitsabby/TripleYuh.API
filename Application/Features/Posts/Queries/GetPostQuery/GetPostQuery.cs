using Application.Models.Posts;
using MediatR;

namespace Application.Features.Posts.Queries.GetPostQuery
{
    public class GetPostQuery : IRequest<PostResponse>
    {
        public int Id { get; set; }
    }
}
