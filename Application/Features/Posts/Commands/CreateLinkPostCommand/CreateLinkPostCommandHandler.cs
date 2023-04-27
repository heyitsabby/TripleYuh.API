using Application.Common.Interfaces;
using Application.Models.Posts;
using MediatR;

namespace Application.Features.Posts.Commands.CreateLinkPostCommand
{
    public class CreateLinkPostCommandHandler : IRequestHandler<CreateLinkPostCommand, PostResponse>
    {
        private readonly IPostService postService;

        public CreateLinkPostCommandHandler(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task<PostResponse> Handle(CreateLinkPostCommand request, CancellationToken cancellationToken)
        {
            var post = await postService.CreateLinkPostAsync(request.Username, request.Title, request.Url);

            return post;
        }
    }
}
