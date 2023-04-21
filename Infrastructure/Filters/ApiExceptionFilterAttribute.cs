using Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(AuthenticateException), HandleAuthenticateException },
                { typeof(CreateResourceException), HandleCreateResourceException },
                { typeof(NotFoundResourceException), HandleNotFoundResourceException },
                { typeof(InvalidTokenException), HandleInvalidTokenException },
                { typeof(UpdateResourceException), HandleUpdateResourceException },
                { typeof(EmailVerificationException), HandleEmailVerificationException }
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            //Log.Error(context.Exception, "Handling exception:");

            Type type = context.Exception.GetType();

            if (exceptionHandlers.ContainsKey(type))
            {
                exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request."
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleAuthenticateException(ExceptionContext context)
        {
            var exception = context.Exception as AuthenticateException;

            var details = new ProblemDetails()
            {
                Title = "Failed to authenticate.",
                Detail = exception?.Message
            };

            context.Result = new UnprocessableEntityObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleCreateResourceException(ExceptionContext context)
        {
            var exception = context.Exception as CreateResourceException;

            var details = new ProblemDetails()
            {
                Title = "Failed to create resource.",
                Detail = exception?.Message
            };

            context.Result = new UnprocessableEntityObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundResourceException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundResourceException;

            var details = new ProblemDetails()
            {
                Title = "Failed to find resource.",
                Detail = exception?.Message
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleInvalidTokenException(ExceptionContext context)
        {
            var exception = context.Exception as InvalidTokenException;

            var details = new ProblemDetails()
            {
                Title = "Invalid Token.",
                Detail = exception?.Message
            };

            context.Result = new UnauthorizedObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleUpdateResourceException(ExceptionContext context)
        {
            var exception = context.Exception as UpdateResourceException;

            var details = new ProblemDetails()
            {
                Title = "Failed to update resource.",
                Detail = exception?.Message
            };

            context.Result = new UnprocessableEntityObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleEmailVerificationException(ExceptionContext context)
        {
            var exception = context.Exception as EmailVerificationException;

            var details = new ProblemDetails
            {
                Title = "Failed to verify email.",
                Detail = exception?.Message
            };

            context.Result = new UnauthorizedObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
