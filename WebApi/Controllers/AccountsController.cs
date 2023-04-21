using Application.Common.Interfaces;
using Application.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ApiControllerBase
    {
        private readonly IAccountService accountService;

        public AccountsController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
    }
}
