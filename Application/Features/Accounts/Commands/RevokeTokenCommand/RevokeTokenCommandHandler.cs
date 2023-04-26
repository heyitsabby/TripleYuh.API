using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Accounts.Commands.RevokeTokenCommand
{
    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand>
    {
        private readonly IAccountService accountService;

        public RevokeTokenCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            await accountService.RevokeTokenAsync(request.Token, request.IpAddress);
        }
    }
}
