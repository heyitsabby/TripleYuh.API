﻿using Domain.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Account : AuditableEntity
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public int Reputation { get; set; } = AccountRules.DefaultReputation;

        public bool AcceptTerms { get; set; }

        public Role Role { get; set; }

        public string? VerificationToken { get; set; }

        public DateTime? Verified { get; set; }
        
        public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;

        public string? ResetToken { get; set; }
        
        public DateTime? ResetTokenExpires { get; set; }
        
        public DateTime? PasswordReset { get; set; }
        
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        public List<Post> Posts { get; set; } = new List<Post>();

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }

    
}
