using Application.Common.Interfaces;
using Application.Models.Accounts;
using MediatR;

namespace Application.Features.Accounts.Commands.UpdateCommand
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, AccountResponse>
    {
        private readonly IAccountService accountService;

        public UpdateCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<AccountResponse> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var account = await accountService.UpdateAsync(
                request.Username, request.Password, request.Role, request.Email, request.Origin);

            return account;
        }
    }
}
