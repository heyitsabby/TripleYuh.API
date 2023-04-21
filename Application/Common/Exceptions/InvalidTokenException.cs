namespace Application.Common.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException() { }

        public InvalidTokenException(string message) : base(message) { }

        public InvalidTokenException(string message, Exception exception) : base(message, exception)
        {

        }

        public InvalidTokenException(string name, object key) : base($"Entity \"{name}\" ({key}) is invalid.")
        {

        }
    }
}
