namespace Application.Common.Exceptions
{
    public class NotFoundResourceException : Exception
    {
        public NotFoundResourceException() { }

        public NotFoundResourceException(string message) : base(message) { }

        public NotFoundResourceException(string message, Exception exception) : base(message, exception)
        {

        }

        public NotFoundResourceException(string name, object key) : base($"Entity \"{name}\" ({key}) could not be found.")
        {

        }
    }
}
