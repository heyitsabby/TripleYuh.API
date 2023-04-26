using Application.Common.Interfaces;
using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Accounts.Commands.UpdateAccountCommand
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, AccountResponse>
    {
        private readonly IAccountService accountService;

        public UpdateAccountCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<AccountResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await accountService.UpdateAsync(
                request.Username, request.Password, request.Role, request.Email, request.Origin);

            return account;
        }
    }
}
