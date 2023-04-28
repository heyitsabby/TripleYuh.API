using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Accounts.Queries.GetAllAccountsQuery
{
    public class GetAllQuery : IRequest<IEnumerable<AccountResponse>>
    {
    }
}
