using MediatR;

namespace Application.Features.Accounts.Commands.DeleteAccountCommand
{
    public class DeleteAccountCommand : IRequest
    {
        public string Username { get; set; } = string.Empty;
    }
}
