using Application.Models.Posts;
using MediatR;

namespace Application.Features.Posts.Commands.UpdatePostCommand
{
    public class UpdatePostCommand : IRequest<PostResponse>
    {
        public int Id { get; set; }

        public string? Body { get; set; }

        public string? Username { get; set; }
    }
}
