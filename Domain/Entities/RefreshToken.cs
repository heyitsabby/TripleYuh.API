namespace Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public Account Account { get; set; } = new Account();
        
        public string Token { get; set; } = string.Empty;

        public DateTime Expires { get; set; }

        public DateTime Created { get; set; }

        public string CreatedByIp { get; set; } = string.Empty;

        public DateTime? Revoked { get; set; }

        public string RevokedByIp { get; set; } = string.Empty;

        public string? ReplacedByToken { get; set; }

        public string? ReasonRevoked { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;

        public bool IsRevoked => Revoked != null;

        public bool IsActive => Revoked == null && !IsExpired;
    }
}
