using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(Account account);

        public int? ValidateJwtToken(string? token);
        
        public RefreshToken GenerateRefreshToken(string? ipAddress);
    }
}
