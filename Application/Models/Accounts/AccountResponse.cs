namespace Application.Models.Accounts
{
    public class AccountResponse
    {
        public string Username { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public DateTime Created { get; set; }
        
        public DateTime? Updated { get; set; }
        
        public bool IsVerified { get; set; }
    }
}
