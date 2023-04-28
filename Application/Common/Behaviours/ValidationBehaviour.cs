using Application.Common.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .Where(result => result.Errors.Any())
                    .SelectMany(result => result.Errors)
                    .ToList();

                if (failures.Any())
                {

                    throw new ValidateResourceException(failures);
                }
            }

            return await next();
        }
    }
}
