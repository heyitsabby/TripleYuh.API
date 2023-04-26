using Application.Common.Interfaces;
using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Accounts.Queries.GetAllQuery
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<AccountResponse>>
    {
        private readonly IAccountService accountService;

        public GetAllQueryHandler(IAccountService accountService)
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
