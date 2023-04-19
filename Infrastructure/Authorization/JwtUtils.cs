using Application.Common.Interfaces;
using Application.Helpers;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authorization
{
    public class JwtUtils : IJwtUtils
    {
        private readonly DataContext context;
        private readonly AppSettings appsettings;

        public JwtUtils(DataContext context, AppSettings appsettings)
        {
            this.context = context;
            this.appsettings = appsettings;
        }

        public string GenerateJwtToken(Account account)
        {
            // generate token that is valid for 15 minutes
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(appsettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            throw new NotImplementedException();
        }

        public int? ValidateJwtToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
