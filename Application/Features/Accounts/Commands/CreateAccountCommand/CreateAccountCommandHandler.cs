using Application.Common.Interfaces;
using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Accounts.Commands.CreateAccountCommand
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountResponse>
    {
        private readonly IAccountService accountService;

        public CreateAccountCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<AccountResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await accountService.CreateAsync(request.Username, request.Role, request.Email, request.Password);

            return account;
        }
    }
}
