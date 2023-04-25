using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Accounts.Commands.ForgotPasswordCommand
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
    {
        private readonly IAccountService accountService;

        public ForgotPasswordCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            await accountService.ForgotPasswordAsync(request.Email, request.Origin);
        }
    }
}
