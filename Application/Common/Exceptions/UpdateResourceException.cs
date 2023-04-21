namespace Application.Common.Exceptions
{
    public class UpdateResourceException : Exception
    {
        public UpdateResourceException() { }

        public UpdateResourceException(string message) : base(message) { }

        public UpdateResourceException(string message, Exception exception) : base(message, exception)
        {

        }

        public UpdateResourceException(string name, object key) : base($"Entity \"{name}\" ({key}) could not be updated.")
        {

        }
    }
}
