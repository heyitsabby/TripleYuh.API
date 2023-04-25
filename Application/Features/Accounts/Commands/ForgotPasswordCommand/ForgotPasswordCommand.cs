using MediatR;

namespace Application.Features.Accounts.Commands.ForgotPasswordCommand
{
    public class ForgotPasswordCommand : IRequest
    {
        public string Email { get; set; } = string.Empty;

        public string? Origin { get; set; }
    }
}
