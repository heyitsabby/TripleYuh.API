using MediatR;

namespace Application.Features.Accounts.Commands.RevokeTokenCommand
{
    public class RevokeTokenCommand : IRequest
    {
        public string? Token { get; set; }

        public string? IpAddress { get; set; }
    }
}
