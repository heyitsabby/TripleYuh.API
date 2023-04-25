using Application.Common.Interfaces;
using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Accounts.Queries.GetByUsernameQuery
{
    public class GetByUsernameQueryHandler : IRequestHandler<GetByUsernameQuery, AccountResponse>
    {
        private readonly IAccountService accountService;

        public GetByUsernameQueryHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<AccountResponse> Handle(GetByUsernameQuery request, CancellationToken cancellationToken)
        {
            var account = await accountService.GetByUsernameAsync(request.Username);

            return account;
        }
    }
}
