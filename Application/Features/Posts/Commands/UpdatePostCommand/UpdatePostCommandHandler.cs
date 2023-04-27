using Application.Common.Interfaces;
using Application.Models.Posts;
using MediatR;

namespace Application.Features.Posts.Commands.UpdatePostCommand
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, PostResponse>
    {
        private readonly IPostService postService;

        public UpdatePostCommandHandler(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task<PostResponse> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await postService.UpdatePostAsync(request.Id, request.Username, request.Body);

            return post;
        }
    }
}
