using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comments.DeleteCommentCommand
{
    internal class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
    {
        private readonly ICommentService commentService;

        public DeleteCommentCommandHandler(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            await commentService.DeleteAsync(request.CommentId, request.Username);
        }
    }
}
