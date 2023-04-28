using Application.Common.Interfaces;
using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Accounts.Queries.GetAccountByUsername
{
    public class GetAccountByUsernameHandler : IRequestHandler<GetAccountByUsernameQuery, AccountResponse>
    {
        private readonly IAccountService accountService;

        public GetAccountByUsernameHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<AccountResponse> Handle(GetAccountByUsernameQuery request, CancellationToken cancellationToken)
        {
            var account = await accountService.GetByUsernameAsync(request.Username);

            return account;
        }
    }
}
