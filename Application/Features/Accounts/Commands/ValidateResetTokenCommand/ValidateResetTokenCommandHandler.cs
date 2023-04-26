using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Accounts.Commands.ValidateResetTokenCommand
{
    public class ValidateResetTokenCommandHandler : IRequestHandler<ValidateResetTokenCommand>
    {
        private readonly IAccountService accountService;

        public ValidateResetTokenCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task Handle(ValidateResetTokenCommand request, CancellationToken cancellationToken)
        {
            await accountService.ValidateResetTokenAsync(request.Token);
        }
    }
}
