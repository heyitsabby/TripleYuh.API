using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Posts.Commands.DeletePostCommand
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IPostService postService;

        public DeletePostCommandHandler(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            await postService.DeleteAsync(request.Id, request.Username);
        }
    }
}
