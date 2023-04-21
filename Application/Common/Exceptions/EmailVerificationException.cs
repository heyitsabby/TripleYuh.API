namespace Application.Common.Exceptions
{
    public class EmailVerificationException : Exception
    {
        public EmailVerificationException() { }

        public EmailVerificationException(string message) : base(message) { }

        public EmailVerificationException(string message, Exception exception) : base(message, exception)
        {

        }

        public EmailVerificationException(string name, object key) : base($"Entity \"{name}\" ({key}) could not be verified.")
        {

        }
    }
}
