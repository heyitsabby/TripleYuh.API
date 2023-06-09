﻿using Application.Common.Security;
using Application.Features.Accounts.Commands.AuthenticateCommand;
using Application.Features.Accounts.Commands.CreateAccountCommand;
using Application.Features.Accounts.Commands.DeleteAccountCommand;
using Application.Features.Accounts.Commands.ForgotPasswordCommand;
using Application.Features.Accounts.Commands.RefreshTokenCommand;
using Application.Features.Accounts.Commands.RegisterCommand;
using Application.Features.Accounts.Commands.ResetPasswordCommand;
using Application.Features.Accounts.Commands.RevokeTokenCommand;
using Application.Features.Accounts.Commands.UpdateAccountCommand;
using Application.Features.Accounts.Commands.ValidateResetTokenCommand;
using Application.Features.Accounts.Commands.VerifyEmailCommand;
using Application.Features.Accounts.Queries.GetAllAccountsQuery;
using Application.Features.Accounts.Queries.GetAccountByUsername;
using Application.Models.Accounts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    public class AccountsController : ApiControllerBase
    {
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> AuthenticateAsync(AuthenticateCommand command)
        {
            command.IpAddress = ipAddress();

            var response = await Mediator.Send(command);

            setTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterCommand command)
        {
            command.Origin = Request.Headers["origin"];

            await Mediator.Send(command);

            return Ok(new { message = "Registration successful, please check your email for verification instructions." });
        }

        [AllowAnonymous]
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmailAsync(VerifyEmailCommand command)
        {
            await Mediator.Send(command);

            return Ok(new { message = "Verification successful, you can now login." });

        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordCommand command)
        {
            command.Origin = Request.Headers["origin"];

            await Mediator.Send(command);

            return Ok(new { message = "Please check your email for password reset instructions." });
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordCommand command)
        {
            await Mediator.Send(command);

            return Ok(new { message = "Password reset successful, you can now login." });
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<AccountResponse>> GetByUsernameAsync(string username)
        {
            // users can get their own account and admins can get any account
            if (username != Account?.Username && Account?.Role != Role.Admin)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var query = new GetAccountByUsernameQuery { Username = username };

            var account = await Mediator.Send(query);

            return Ok(account);
        }

        [HttpPut("{username}")]
        public async Task<ActionResult<AccountResponse>> UpdateAsync(string username, UpdateAccountCommand command)
        {
            // users can update their own account and admins can update any account
            if (username != Account?.Username && Account?.Role != Role.Admin)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            if (Account.Role != Role.Admin)
            {
                command.Role = null;
            }

            command.Username = username;

            command.Origin = Request.Headers["origin"];

            var account = await Mediator.Send(command);

            return Ok(account);
        }

        [Authorize(Role.Admin)]
        [HttpPost] 
        public async Task<ActionResult<AccountResponse>> CreateAsync(CreateAccountCommand command)
        {
            var account = await Mediator.Send(command);

            return Ok(account);
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteAsync(string username)
        {
            // users can delete their own account and admins can delete any account
            if (username != Account?.Username && Account?.Role != Role.Admin)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var command = new DeleteAccountCommand { Username = username };

            await Mediator.Send(command);

            return Ok(new { message = "Account deleted successfully." });
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthenticateResponse>> RefreshTokenAsync()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var command = new RefreshTokenCommand { RefreshToken = refreshToken, IpAddress = ipAddress() };

            var response = await Mediator.Send(command);

            setTokenCookie(response.RefreshToken);

            return response;
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeTokenAsync(RevokeTokenCommand command)
        {
            // accept token from request body or cookie
            command.Token ??= Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(command.Token))
            {
                return BadRequest(new { message = "Token is required" });
            }

            // users can revoke their own tokens and admins can revoke any tokens
            if (!Account.OwnsToken(command.Token) && Account.Role != Role.Admin)
            {
                return Unauthorized(new { message = "Unsauthorized" });
            }

            command.IpAddress = ipAddress();

            await Mediator.Send(command);

            return Ok(new { message = "Token revoked" });
        }

        [AllowAnonymous]
        [HttpPost("validate-reset-token")]
        public async Task<IActionResult> ValidateResetTokenAsync(ValidateResetTokenCommand command)
        {
            await Mediator.Send(command);

            return Ok(new { message = "Reset token is valid." });
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAllAsync()
        {
            var query = new GetAllQuery();

            var accounts = await Mediator.Send(query);

            return Ok(accounts);
        }

        // Helpers

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string? ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext?.Connection?.RemoteIpAddress?.MapToIPv4().ToString();
        }
    }
}
