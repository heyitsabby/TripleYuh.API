namespace Application.Common.Exceptions
{
    public class CreateResourceException : Exception
    {
        public CreateResourceException() { }

        public CreateResourceException(string message) : base(message) { }

        public CreateResourceException(string message, Exception exception) : base(message, exception)
        {

        }

        public CreateResourceException(string name, object key) : base($"Entity \"{name}\" ({key}) could not be created.")
        {

        }
    }
}
