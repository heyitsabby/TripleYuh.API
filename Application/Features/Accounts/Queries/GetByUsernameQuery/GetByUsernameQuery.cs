using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Accounts.Queries.GetByUsernameQuery
{
    public class GetByUsernameQuery : IRequest<AccountResponse>
    {
        public string Username { get; set; } = string.Empty;
    }
}
