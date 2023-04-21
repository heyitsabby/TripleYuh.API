using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Helpers;
using Application.Models.Accounts;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataContext context;
        private readonly IJwtUtils jwtUtils;
        private readonly IOptions<AppSettings> appSettings;
        private readonly IMapper mapper;

        public AccountService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings,
            IMapper mapper)
        {
            this.context = context;
            this.jwtUtils = jwtUtils;
            this.appSettings = appSettings;
            this.mapper = mapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var account = context.Accounts.SingleOrDefault(account => account.Email == model.Email);

            // validate
            if (account == null || !account.IsVerified || !BCrypt.Net.BCrypt.Verify(model.Password, account.PasswordHash))
                throw new AuthenticateException("Email or password is incorrect.");

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = jwtUtils.GenerateJwtToken(account);

            var refreshToken = jwtUtils.GenerateRefreshToken(ipAddress);

            account.RefreshTokens.Add(refreshToken);

            removeOldRefreshTokens(account);

            context.Update(account);
            context.SaveChanges();

            var response = mapper.Map<AuthenticateResponse>(account);
            
            response.JwtToken = jwtToken;

            response.RefreshToken = refreshToken.Token;

            return response;
        }

        public AccountResponse Create(CreateRequest model)
        {
            // validate
            if (context.Accounts.Any(account => account.Email == model.Email))
            {
                throw new CreateResourceException($"Email '{model.Email}' is already registered.");
            }

            // map model to new account object
            var account = mapper.Map<Account>(model);
            
            account.Created = DateTime.UtcNow;

            account.Verified = DateTime.UtcNow;

            account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            context.Accounts.Add(account);

            context.SaveChanges();

            return mapper.Map<AccountResponse>(account);

        }

        public void Delete(int id)
        {
            var account = getAccount(id);

            context.Accounts.Remove(account);

            context.SaveChanges();
        }

        public void ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = context.Accounts.SingleOrDefault(a => a.Email == model.Email);

            // always return ok response to prevent email enumeration
            if (account == null) { return; }

            // create reset token that expires after 1 day
            account.ResetToken = generateResetToken();

            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

            context.Accounts.Update(account);
            
            context.SaveChanges();

            // send email
            sendPasswordResetEmail(account, origin);
        }

        public IEnumerable<AccountResponse> GetAll()
        {
            var accounts = context.Accounts;

            return mapper.Map<IList<AccountResponse>>(accounts);
        }

        public AccountResponse GetById(int id)
        {
            var account = getAccount(id);

            return mapper.Map<AccountResponse>(account);
        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            var account = getAccountByRefreshToken(token);

            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsRevoked)
            {
                // revoke all descendant tokens in case this token has been compromised
                revokeDescendantRefreshTokens(refreshToken, account, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");

                context.Update(account);

                context.SaveChanges();
            }

            if (!refreshToken.IsActive) { throw new InvalidTokenException("Refresh token is invalid."); }

            // replace old refresh token with a new one (rotate token)
            var newRefreshToken = rotateRefreshToken(refreshToken, ipAddress);
            
            account.RefreshTokens.Add(newRefreshToken);

            // remove old refresh tokens from account
            removeOldRefreshTokens(account);

            // save changes to db
            context.Update(account);
            context.SaveChanges();

            // generate new jwt
            var jwtToken = jwtUtils.GenerateJwtToken(account);

            // return data in authenticate response object
            var response = mapper.Map<AuthenticateResponse>(account);

            response.JwtToken = jwtToken;
            
            response.RefreshToken = newRefreshToken.Token;
            
            return response;

        }

        public void Register(RegisterRequest model, string origin)
        {
            // validate
            if (context.Accounts.Any(x => x.Email == model.Email))
            {
                // send already registered error in email to prevent account enumeration
                sendAlreadyRegisteredEmail(model.Email, origin);
                return;
            }

            // map model to new account object
            var account = mapper.Map<Account>(model);

            // first registered account is an admin
            var isFirstAccount = context.Accounts.Count() == 0;

            account.Role = isFirstAccount ? Role.Admin : Role.User;
            
            account.Created = DateTime.UtcNow;
            
            account.VerificationToken = generateVerificationToken();

            // hash password
            account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // save account
            context.Accounts.Add(account);
            context.SaveChanges();

            // send email
            sendVerificationEmail(account, origin);
        }

        public void ResetPassword(ResetPasswordRequest model)
        {
            var account = getAccountByResetToken(model.Token);

            // update password and remove reset token
            account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            
            account.PasswordReset = DateTime.UtcNow;
            
            account.ResetToken = null;
            
            account.ResetTokenExpires = null;

            context.Accounts.Update(account);
            
            context.SaveChanges();
        }

        public void RevokeToken(string token, string ipAddress)
        {
            var account = getAccountByRefreshToken(token);
            
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive) { throw new InvalidTokenException("Invalid refresh token"); }

            // revoke token and save
            revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            
            context.Update(account);
            
            context.SaveChanges();
        }

        public AccountResponse Update(int id, UpdateRequest model)
        {
            var account = getAccount(id);

            // validate
            if (account.Email != model.Email && context.Accounts.Any(x => x.Email == model.Email))
                throw new UpdateResourceException($"Email '{model.Email}' is already registered.");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
            {
                account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            }

            // copy model to account and save
            mapper.Map(model, account);
            
            account.Updated = DateTime.UtcNow;
            
            context.Accounts.Update(account);
            
            context.SaveChanges();

            return mapper.Map<AccountResponse>(account);
        }

        public void ValidateResetToken(ValidateResetTokenRequest model)
        {
            getAccountByResetToken(model.Token);
        }

        public void VerifyEmail(string token)
        {
            var account = context.Accounts.SingleOrDefault(x => x.VerificationToken == token) 
                ?? throw new EmailVerificationException("Email verification failed.");
            
            account.Verified = DateTime.UtcNow;
            
            account.VerificationToken = null;

            context.Accounts.Update(account);
            
            context.SaveChanges();
        }

        // Helpers

        private void removeOldRefreshTokens(Account account)
        {
            account.RefreshTokens.RemoveAll(
                x => !x.IsActive &&
                x.Created.AddDays(appSettings.Value.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        private Account getAccount(int id)
        {
            var account = context.Accounts.Find(id);

            return account == null ? throw new NotFoundResourceException("Account not found.") : account;
        }

        private string generateResetToken()
        {
            // token is a cryptographically strong random sequence of values
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            var tokenIsUnqiue = !context.Accounts.Any(a => a.ResetToken == token);

            if (!tokenIsUnqiue)
            {
                return generateResetToken();
            }

            return token;
        }

        private void sendPasswordResetEmail(Account account, string origin)
        {
            // TODO: Send email
        }

        private Account getAccountByRefreshToken(string token)
        {
            var account = context.Accounts.SingleOrDefault(a => a.RefreshTokens.Any(t => t.Token == token));

            return account ?? throw new InvalidTokenException("Refresh token is invalid.");
        }

        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, Account account, string ipAddress, string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = account.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);

                if (childToken.IsActive)
                {
                    revokeRefreshToken(childToken, ipAddress, reason);
                } else
                {
                    revokeDescendantRefreshTokens(childToken, account, ipAddress, reason);
                }
            }
        }

        private void revokeRefreshToken(RefreshToken token, string ipAddress, string? reason = null, string? replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;

            token.RevokedByIp = ipAddress;
            
            token.ReasonRevoked = reason;
            
            token.ReplacedByToken = replacedByToken;
        }

        private RefreshToken rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = jwtUtils.GenerateRefreshToken(ipAddress);

            revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            
            return newRefreshToken;
        }

        private void sendAlreadyRegisteredEmail(string email, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
                message = $@"<p>If you don't know your password please visit the <a href=""{origin}/account/forgot-password"">forgot password</a> page.</p>";
            else
                message = "<p>If you don't know your password you can reset it via the <code>/accounts/forgot-password</code> api route.</p>";

            emailService.Send(
                to: email,
                subject: "TripleYuh API - Email Already Registered",
                html: $@"<h2>Email Already Registered</h2>
                        <p>Your email <strong>{email}</strong> is already registered.</p>
                        {message}"
            );
        }

        private string generateVerificationToken()
        {
            // token is a cryptographically strong random sequence of values
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            // ensure token is unique by checking against db
            var tokenIsUnique = !context.Accounts.Any(x => x.VerificationToken == token);

            if (!tokenIsUnique) { return generateVerificationToken(); }

            return token;
        }

        private void sendVerificationEmail(Account account, string origin)
        {
            string message;
            
            if (!string.IsNullOrEmpty(origin))
            {
                // origin exists if request sent from browser single page app (e.g. Angular or React)
                // so send link to verify via single page app
                var verifyUrl = $"{origin}/account/verify-email?token={account.VerificationToken}";
                
                message = $@"<p>Please click the below link to verify your email address:</p>
                            <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
            }
            else
            {
                // origin missing if request sent directly to api (e.g. from Postman)
                // so send instructions to verify directly with api
                message = $@"<p>Please use the below token to verify your email address with the <code>/accounts/verify-email</code> api route:</p>
                            <p><code>{account.VerificationToken}</code></p>";
            }

            emailService.Send(
                to: account.Email,
                subject: "Sign-up Verification API - Verify Email",
                html: $@"<h4>Verify Email</h4>
                        <p>Thanks for registering!</p>
                        {message}");
        }

        private Account getAccountByResetToken(string token)
        {
            var account = context.Accounts.SingleOrDefault(x =>
                x.ResetToken == token && x.ResetTokenExpires > DateTime.UtcNow);

            return account ?? throw new InvalidTokenException("Invalid reset token");
        }
    }
}
