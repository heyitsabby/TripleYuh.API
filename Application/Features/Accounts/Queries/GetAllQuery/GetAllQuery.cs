using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Accounts.Queries.GetAllQuery
{
    public class GetAllQuery : IRequest<IEnumerable<AccountResponse>>
    {
    }
}
