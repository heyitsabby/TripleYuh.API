using FluentValidation.Results;

namespace Application.Common.Exceptions
{
    public class ValidateResourceException : Exception
    {
        public IEnumerable<string> Errors = new List<string>();

        public ValidateResourceException() { }

        public ValidateResourceException(string message) : base(message)
        {
        }

        public ValidateResourceException(string message, Exception exception) : base(message, exception)
        {

        }

        public ValidateResourceException(string name, object key) : base($"Entity \"{name}\" ({key}) could not be created.")
        {

        }

        public ValidateResourceException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures.Select(failure => failure.ErrorMessage);
        }
    }
}
