using Application.Common.Interfaces;
using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Accounts.Queries.GetAllAccountsQuery
{
    public class GetAllAcccountsQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<AccountResponse>>
    {
        private readonly IAccountService accountService;

        public GetAllAcccountsQueryHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<IEnumerable<AccountResponse>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var accounts = await accountService.GetAllAsync();

            return accounts;
        }
    }
}
