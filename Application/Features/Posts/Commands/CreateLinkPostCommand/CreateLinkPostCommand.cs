using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Posts.Commands.CreateLinkPostCommand
{
    public class CreateLinkPostCommand : IRequest<PostResponse>
    {
        public string? Username { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;
    }
}
