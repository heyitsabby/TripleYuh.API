using Application.Common.Interfaces;
using Application.Helpers;
using Infrastructure.Persistence;
using Microsoft.Extensions.Options;

namespace WebApi.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;
        private readonly AppSettings appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            this.next = next;
            this.appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, DataContext dataContext, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var accountId = jwtUtils.ValidateJwtToken(token);
            if (accountId != null)
            {
                // attach account to context on successful jwt validation
                context.Items["Account"] = await dataContext.Accounts.FindAsync(accountId.Value);
            }

            await next(context);
        }
    }
}
