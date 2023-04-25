using Application.Common.Interfaces;
using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Accounts.Commands.RefreshTokenCommand
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthenticateResponse>
    {
        private readonly IAccountService accountService;

        public RefreshTokenCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<AuthenticateResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var response = await accountService.RefreshTokenAsync(request.RefreshToken, request.IpAddress);

            return response;
        }
    }
}
