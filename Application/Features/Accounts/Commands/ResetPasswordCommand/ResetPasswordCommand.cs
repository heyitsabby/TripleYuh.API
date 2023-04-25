using MediatR;

namespace Application.Features.Accounts.Commands.ResetPasswordCommand
{
    public class ResetPasswordCommand : IRequest
    {
        public string Token { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
