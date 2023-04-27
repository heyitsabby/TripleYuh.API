using Application.Models.Posts;
using MediatR;

namespace Application.Features.Posts.Queries.GetAllPostsQuery
{
    public class GetAllPostsQuery : IRequest<IEnumerable<PostResponse>>
    {
    }
}
