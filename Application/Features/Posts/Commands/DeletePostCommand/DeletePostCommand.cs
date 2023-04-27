using MediatR;

namespace Application.Features.Posts.Commands.DeletePostCommand
{
    public class DeletePostCommand : IRequest
    {
        public int Id { get; set; }

        public string? Username { get; set; }
    }
}
