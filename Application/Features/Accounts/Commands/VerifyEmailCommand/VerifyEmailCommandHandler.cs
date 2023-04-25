using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Accounts.Commands.VerifyEmailCommand
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand>
    {
        private readonly IAccountService accountService;

        public VerifyEmailCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            await accountService.VerifyEmailAsync(request.Token);
        }
    }
}
