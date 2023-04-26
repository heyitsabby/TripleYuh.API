using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Accounts.Commands.DeleteAccountCommand
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
    {
        private readonly IAccountService accountService;

        public DeleteAccountCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            await accountService.DeleteAsync(request.Username);
        }
    }
}
