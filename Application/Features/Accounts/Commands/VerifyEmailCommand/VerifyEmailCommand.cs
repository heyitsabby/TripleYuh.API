using MediatR;

namespace Application.Features.Accounts.Commands.VerifyEmailCommand
{
    public class VerifyEmailCommand : IRequest
    {
        public string Token { get; set; } = string.Empty;
    }
}
