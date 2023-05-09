using Application.Common.Interfaces;
using Application.Models.Comments;
using MediatR;

namespace Application.Features.Comments.Queries.GetCommentsByUsernameQuery
{
    public class GetCommentsByUsernameQueryHandler : IRequestHandler<GetCommentsByUsernameQuery, IEnumerable<CommentResponse>>
    {
        private readonly ICommentService commentService;

        public GetCommentsByUsernameQueryHandler(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public async Task<IEnumerable<CommentResponse>> Handle(GetCommentsByUsernameQuery request, CancellationToken cancellationToken)
        {
            var response = await commentService.GetAllByUsernameAsync(request.Username);

            return response;
        }
    }
}
