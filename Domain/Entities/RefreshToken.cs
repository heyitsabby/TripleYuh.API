﻿namespace Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public Account Account { get; set; } = new Account();

        public int AccountId { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime Expires { get; set; }

        public DateTime Created { get; set; }

        public string? CreatedByIp { get; set; }

        public DateTime? Revoked { get; set; }

        public string? RevokedByIp { get; set; }

        public string? ReplacedByToken { get; set; }

        public string? ReasonRevoked { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;

        public bool IsRevoked => Revoked != null;

        public bool IsActive => Revoked == null && !IsExpired;
    }
}
