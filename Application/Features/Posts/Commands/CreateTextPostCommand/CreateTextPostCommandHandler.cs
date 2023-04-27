using Application.Common.Interfaces;
using Application.Models.Posts;
using MediatR;

namespace Application.Features.Posts.Commands.CreateTextPostCommand
{
    public class CreateTextPostCommandHandler : IRequestHandler<CreateTextPostCommand, PostResponse>
    {
        private readonly IPostService postService;

        public CreateTextPostCommandHandler(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task<PostResponse> Handle(CreateTextPostCommand request, CancellationToken cancellationToken)
        {
            var post = await postService.CreateTextPostAsync(request.Username, request.Title, request.Body);

            return post;
        }
    }
}
