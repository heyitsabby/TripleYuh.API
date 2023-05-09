using Application.Common.Interfaces;
using Application.Models.Comments;
using MediatR;

namespace Application.Features.Comments.Queries.GetCommentsByPostQuery
{
    public class GetCommentsByPostQueryHandler : IRequestHandler<GetCommentsByPostQuery, IEnumerable<CommentResponse>>
    {
        private readonly ICommentService commentService;

        public GetCommentsByPostQueryHandler(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public async Task<IEnumerable<CommentResponse>> Handle(GetCommentsByPostQuery request, CancellationToken cancellationToken)
        {
            var response = await commentService.GetAllByPostAsync(request.PostId);

            return response;
        }
    }
}
