using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Accounts.Commands.ResetPasswordCommand
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly IAccountService accountService;

        public ResetPasswordCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            await accountService.ResetPasswordAsync(request.Token, request.Password);
        }
    }
}
