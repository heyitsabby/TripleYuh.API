using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Accounts.Queries.GetAccountByUsername;

public class GetAccountByUsernameQuery : IRequest<AccountResponse>
{
    public string Username { get; set; } = string.Empty;
}
