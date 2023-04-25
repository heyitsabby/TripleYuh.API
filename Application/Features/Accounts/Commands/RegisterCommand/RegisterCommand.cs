using MediatR;

namespace Application.Features.Accounts.Commands.RegisterCommand
{
    public class RegisterCommand : IRequest
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;

        public bool AcceptTerms { get; set; }

        public string? Origin { get; set; }
    }
}
