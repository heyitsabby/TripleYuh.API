using System.Text.Json.Serialization;

namespace Application.Models.Accounts
{
    public class AuthenticateResponse
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public bool IsVerified { get; set; }

        public string JwtToken { get; set; } = string.Empty;

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; } = string.Empty;
    }
}
