namespace Application.Common.Exceptions
{
    public class AuthenticateException : Exception
    {
        public AuthenticateException()
        {

        }

        public AuthenticateException(string message) : base(message)
        {

        }

        public AuthenticateException(string message, Exception exception) : base(message, exception)
        {

        }

        public AuthenticateException(string name, object key) : base($"Entity \"{name}\" ({key}) could not be authenticated.")
        {

        }
    }
}
