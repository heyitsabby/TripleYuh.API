using Application.Common.Interfaces;
using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Accounts.Commands.AuthenticateCommand
{
    internal class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticateResponse>
    {
        private readonly IAccountService accountService;

        public AuthenticateCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public Task<AuthenticateResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var response = accountService.AuthenticateAsync(request.Email, request.Password, request.IpAddress);

            return response;
        }
    }
}
