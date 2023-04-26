using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Helpers;
using Application.Models.Accounts;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;
using System.Security.Cryptography;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataContext context;
        private readonly IJwtUtils jwtUtils;
        private readonly IEmailService emailService;
        private readonly IOptions<AppSettings> appSettings;
        private readonly IMapper mapper;

        public AccountService(
            DataContext context,
            IJwtUtils jwtUtils,
            IEmailService emailService,
            IOptions<AppSettings> appSettings,
            IMapper mapper)
        {
            this.context = context;
            this.jwtUtils = jwtUtils;
            this.emailService = emailService;
            this.appSettings = appSettings;
            this.mapper = mapper;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(string email, string password, string? ipAddress)
        {
            var account = await context.Accounts.SingleOrDefaultAsync(account => account.Email == email);

            // validate
            if (account == null || !account.IsVerified || !BCrypt.Net.BCrypt.Verify(password, account.PasswordHash))
                throw new AuthenticateException("Email or password is incorrect.");

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = jwtUtils.GenerateJwtToken(account);

            var refreshToken = jwtUtils.GenerateRefreshToken(ipAddress);

            account.RefreshTokens.Add(refreshToken);

            removeOldRefreshTokens(account);

            context.Update(account);

            await context.SaveChangesAsync();

            var response = mapper.Map<AuthenticateResponse>(account);

            response.JwtToken = jwtToken;

            response.RefreshToken = refreshToken.Token;

            return response;
        }

        public async Task<AccountResponse> CreateAsync(CreateRequest model)
        {
            // validate
            if (await context.Accounts.AnyAsync(account => account.Email == model.Email))
            {
                throw new CreateResourceException($"Email '{model.Email}' is already registered.");
            }

            // map model to new account object
            var account = mapper.Map<Account>(model);

            account.Created = DateTime.UtcNow;

            account.Verified = DateTime.UtcNow;

            account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            context.Accounts.Add(account);

            await context.SaveChangesAsync();

            return mapper.Map<AccountResponse>(account);

        }

        public async Task DeleteAsync(string username)
        {
            var account = await getAccountAsync(username);

            context.Accounts.Remove(account);

            await context.SaveChangesAsync();
        }

        public async Task ForgotPasswordAsync(string email, string origin)
        {
            var account = context.Accounts.SingleOrDefault(a => a.Email == email);

            // always return ok response to prevent email enumeration
            if (account == null) { return; }

            // create reset token that expires after 1 day
            account.ResetToken = await generateResetTokenAsync();

            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

            context.Accounts.Update(account);

            await context.SaveChangesAsync();

            // send email
            sendPasswordResetEmail(account, origin);
        }

        public async Task<IEnumerable<AccountResponse>> GetAllAsync()
        {
            var accounts = context.Accounts;

            return mapper.Map<IList<AccountResponse>>(accounts);
        }

        public async Task<AccountResponse> GetByUsernameAsync(string username)
        {
            var account = await getAccountAsync(username);

            return mapper.Map<AccountResponse>(account);
        }

        public async Task<AuthenticateResponse> RefreshTokenAsync(string? token, string? ipAddress)
        {
            var account = await getAccountByRefreshTokenAsync(token);

            var refreshToken = account.RefreshTokens.SingleOrDefault(x => x.Token == token)
                ?? throw new InvalidTokenException("Refresh token is null");

            if (refreshToken.IsRevoked)
            {
                // revoke all descendant tokens in case this token has been compromised
                    revokeDescendantRefreshTokens(refreshToken, account, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");

                context.Update(account);

                await context.SaveChangesAsync();
            }

            if (!refreshToken.IsActive) { throw new InvalidTokenException("Refresh token is invalid."); }

            // replace old refresh token with a new one (rotate token)
            var newRefreshToken = rotateRefreshToken(refreshToken, ipAddress);

            account.RefreshTokens.Add(newRefreshToken);

            // remove old refresh tokens from account
            removeOldRefreshTokens(account);

            // save changes to db
            context.Update(account);

            await context.SaveChangesAsync();

            // generate new jwt
            var jwtToken = jwtUtils.GenerateJwtToken(account);

            // return data in authenticate response object
            var response = mapper.Map<AuthenticateResponse>(account);

            response.JwtToken = jwtToken;

            response.RefreshToken = newRefreshToken.Token;

            return response;

        }

        public async Task RegisterAsync(string username, string email, string password, bool acceptTerms, string origin)
        {
            // validate
            if (await context.Accounts.AnyAsync(x => x.Email == email))
            {
                // send already registered error in email to prevent account enumeration
                sendAlreadyRegisteredEmail(email, origin);
                return;
            }

            // map model to new account object
            //var account = mapper.Map<Account>(model);


            var account = new Account
            {
                Username = username,
                Email = email,
                AcceptTerms = acceptTerms
            };

            // first registered account is an admin
            var isFirstAccount = context.Accounts.Count() == 0;

            account.Role = isFirstAccount ? Role.Admin : Role.User;

            account.Created = DateTime.UtcNow;

            account.VerificationToken = await generateVerificationTokenAsync();

            // hash password
            account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

            // save account
            await context.Accounts.AddAsync(account);

            await context.SaveChangesAsync();

            // send email
            sendVerificationEmail(account, origin);
        }

        public async Task ResetPasswordAsync(string token, string password)
        {
            var account = await getAccountByResetTokenAsync(token);

            // update password and remove reset token
            account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

            account.PasswordReset = DateTime.UtcNow;

            account.ResetToken = null;

            account.ResetTokenExpires = null;

            context.Accounts.Update(account);

            await context.SaveChangesAsync();
        }

        public async Task RevokeTokenAsync(string? token, string? ipAddress)
        {
            var account = await getAccountByRefreshTokenAsync(token);

            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive) { throw new InvalidTokenException("Invalid refresh token"); }

            // revoke token and save
            revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");

            context.Update(account);

            await context.SaveChangesAsync();
        }

        public async Task<AccountResponse> UpdateAsync(string username, string? password, string? role, string? email, string? origin)
        {
            var account = await getAccountAsync(username);

            // Validate
            if (account.Email != email &&
                await context.Accounts.AnyAsync(account => account.Email == email))
            {
                throw new UpdateResourceException($"Email '{email}' is already registered.");
            }

            // Update email if it was entered
            if (!string.IsNullOrEmpty(email))
            {
                account.Email = email;
                account.VerificationToken = await generateVerificationTokenAsync();
            }

            // Update role if it is entered by an Admin
            if (!string.IsNullOrEmpty(role))
            {
                if (Enum.TryParse(role, out Role roleAsEnum))
                {
                    account.Role = roleAsEnum;
                }
            }

            // Hash password if it was entered
            if (!string.IsNullOrEmpty(password))
            {
                account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            }

            // Copy model to account and save
            //mapper.Map(model, account);
            
            account.Updated = DateTime.UtcNow;
            
            context.Accounts.Update(account);
            
            await context.SaveChangesAsync();

            // Send verification email
            if (!string.IsNullOrEmpty(email))
            {
                
                sendVerificationEmail(account, origin);
            }

            return mapper.Map<AccountResponse>(account);
        }

        public async Task ValidateResetTokenAsync(string? token)
        {
            await getAccountByResetTokenAsync(token);
        }

        public async Task VerifyEmailAsync(string token)
        {
            var account = await context.Accounts.SingleOrDefaultAsync(x => x.VerificationToken == token) 
                ?? throw new EmailVerificationException("Email verification failed.");
            
            account.Verified = DateTime.UtcNow;
            
            account.VerificationToken = null;

            context.Accounts.Update(account);
            
            await context.SaveChangesAsync();
        }

        // Helpers

        private void removeOldRefreshTokens(Account account)
        {
            account.RefreshTokens.RemoveAll(
                x => !x.IsActive &&
                x.Created.AddDays(appSettings.Value.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        private async Task<Account> getAccountAsync(string username)
        {
            var account = await context.Accounts
                .Where(account => account.Username == username)
                .SingleOrDefaultAsync();

            return account ?? throw new NotFoundResourceException("Account not found.");
        }

        private async Task<string> generateResetTokenAsync()
        {
            // token is a cryptographically strong random sequence of values
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            var tokenIsUnqiue = !await context.Accounts.AnyAsync(a => a.ResetToken == token);

            if (!tokenIsUnqiue)
            {
                return await generateResetTokenAsync();
            }

            return token;
        }

        private void sendPasswordResetEmail(Account account, string? origin)
        {
            string message;

            if (!string.IsNullOrEmpty(origin))
            {
                var resetUrl = $"{origin}/account/reset-password?token={account.ResetToken}";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                            <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to reset your password with the <code>/accounts/reset-password</code> api route:</p>
                            <p><code>{account.ResetToken}</code></p>";
            }

            emailService.Send(
                to: account.Email,
                subject: "Sign-up Verification API - Reset Password",
                html: $@"<h4>Reset Password Email</h4>
                        {message}"
            );
        }

        private async Task<Account> getAccountByRefreshTokenAsync(string? token)
        {
            if (token == null)
            {
                throw new InvalidTokenException("Refresh token is invalid.");
            }

            var account = await context.Accounts.Include(a => a.RefreshTokens).SingleOrDefaultAsync(a => a.RefreshTokens.Any(t => t.Token == token));

            return account ?? throw new InvalidTokenException("Refresh token is invalid.");
        }

        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, Account account, string? ipAddress, string reason)
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

        private void revokeRefreshToken(RefreshToken token, string? ipAddress, string? reason = null, string? replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;

            token.RevokedByIp = ipAddress;
            
            token.ReasonRevoked = reason;
            
            token.ReplacedByToken = replacedByToken;
        }

        private RefreshToken rotateRefreshToken(RefreshToken refreshToken, string? ipAddress)
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

        private async Task<string> generateVerificationTokenAsync()
        {
            // token is a cryptographically strong random sequence of values
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            // ensure token is unique by checking against db
            var tokenIsUnique = !await context.Accounts.AnyAsync(x => x.VerificationToken == token);

            if (!tokenIsUnique) { return await generateVerificationTokenAsync(); }

            return token;
        }

        private void sendVerificationEmail(Account account, string? origin)
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

        private async Task<Account> getAccountByResetTokenAsync(string token)
        {
            var account = await context.Accounts.SingleOrDefaultAsync(x =>
                x.ResetToken == token && x.ResetTokenExpires > DateTime.UtcNow);

            return account ?? throw new InvalidTokenException("Invalid reset token");
        }
    }
}
