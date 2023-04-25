using Application.Models.Accounts;

namespace Application.Common.Interfaces
{
    public interface IAccountService
    {
        Task<AuthenticateResponse> AuthenticateAsync(string email, string password, string? ipAddress);

        Task<AuthenticateResponse> RefreshTokenAsync(string? token, string? ipAddress);
        
        Task RevokeTokenAsync(string? token, string? ipAddress);
        
        Task RegisterAsync(string username, string email, string password, bool acceptTerms, string origin);
        
        Task VerifyEmailAsync(string token);
        
        Task ForgotPasswordAsync(string email, string origin);
        
        Task ValidateResetTokenAsync(ValidateResetTokenRequest model);
        
        Task ResetPasswordAsync(string token, string password);
        
        IEnumerable<AccountResponse> GetAll();
        
        Task<AccountResponse> GetByUsernameAsync(string username);
        
        Task<AccountResponse> CreateAsync(CreateRequest model);
        
        Task<AccountResponse> UpdateAsync(string username, string? password, string? role, string? email, string? origin);
        
        Task DeleteAsync(string username);
    }
}
