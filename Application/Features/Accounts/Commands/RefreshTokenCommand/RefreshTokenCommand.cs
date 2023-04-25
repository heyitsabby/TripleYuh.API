using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Accounts.Commands.RefreshTokenCommand
{
    public class RefreshTokenCommand : IRequest<AuthenticateResponse>
    {
        public string? RefreshToken { get; set; }

        public string? IpAddress { get; set; }
    }
}
