using Application.Common.Interfaces;
using Application.Models.Comments;
using MediatR;

namespace Application.Features.Comments.Commands.CreateCommentCommand
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentResponse>
    {
        private readonly ICommentService commentService;

        public CreateCommentCommandHandler(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public async Task<CommentResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await commentService.CreateAsync(request.PostId, request.Username, request.ParentId, request.Body);

            return comment;
        }
    }
}
