using Application.Common.Interfaces;
using Application.Models.Posts;
using MediatR;

namespace Application.Features.Posts.Queries.GetAllPostsQuery
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<PostResponse>>
    {
        private readonly IPostService postService;

        public GetAllPostsQueryHandler(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task<IEnumerable<PostResponse>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await postService.GetAllAsync();

            return posts;
        }
    }
}
