using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Accounts.Commands.RegisterCommand
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IAccountService accountService;

        public RegisterCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await accountService.RegisterAsync(
                request.Username, request.Email, request.Password, request.AcceptTerms, request.Origin);
        }
    }
}
