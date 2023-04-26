using MediatR;

namespace Application.Features.Accounts.Commands.ValidateResetTokenCommand
{
    public class ValidateResetTokenCommand : IRequest
    {
        public string? Token { get; set; }
    }
}
