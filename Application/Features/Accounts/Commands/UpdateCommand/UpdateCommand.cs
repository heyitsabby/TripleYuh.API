using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Accounts.Commands.UpdateCommand
{
    public class UpdateCommand : IRequest<AccountResponse>
    {
        public string Username { get; set; } = string.Empty;

        public string? Origin { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

        public string? Role { get; set; }

        public string? Email { get; set; }
    }
}
