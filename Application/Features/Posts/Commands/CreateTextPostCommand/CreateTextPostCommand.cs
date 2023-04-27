using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Posts.Commands.CreateTextPostCommand
{
    public class CreateTextPostCommand : IRequest<PostResponse>
    {
        public string Title { get; set; } = string.Empty;

        public string? Body { get; set; }

        public string? Username { get; set; }
    }
}
