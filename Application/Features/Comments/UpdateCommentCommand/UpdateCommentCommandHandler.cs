using Application.Common.Interfaces;
using Application.Models.Comments;
using MediatR;

namespace Application.Features.Comments.UpdateCommentCommand
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, CommentResponse>
    {
        private readonly ICommentService commentService;

        public UpdateCommentCommandHandler(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public async Task<CommentResponse> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var response = await commentService.UpdateAsync(request.Id, request.Username, request.Body);

            return response;
        }
    }
}
