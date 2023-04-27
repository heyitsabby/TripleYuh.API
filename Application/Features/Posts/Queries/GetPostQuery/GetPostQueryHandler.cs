using Application.Common.Interfaces;
using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Posts.Queries.GetPostQuery
{
    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostResponse>
    {
        private readonly IPostService postService;

        public GetPostQueryHandler(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task<PostResponse> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            var post = await postService.GetAsync(request.Id);

            return post;
        }
    }
}
