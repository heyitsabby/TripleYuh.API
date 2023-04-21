using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        private readonly ISender? _mediator;

        protected ISender Mediator => _mediator ?? HttpContext.RequestServices.GetRequiredService<ISender>();

        // returns the current authenticated account (null if not logged in)
        public Account? Account => (Account?)HttpContext.Items["Account"];
    }
}
