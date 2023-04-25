using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Accounts.Commands.AuthenticateCommand
{
    public class AuthenticateCommand : IRequest<AuthenticateResponse>
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string? IpAddress { get; set; }
    }
}
