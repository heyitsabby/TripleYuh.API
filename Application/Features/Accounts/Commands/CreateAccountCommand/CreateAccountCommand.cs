using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Accounts.Commands.CreateAccountCommand
{
    public class CreateAccountCommand : IRequest<AccountResponse>
    {
        public string Username { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
