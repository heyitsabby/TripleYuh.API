namespace Application.Models.Accounts
{
    public class AuthenticateRequest
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
