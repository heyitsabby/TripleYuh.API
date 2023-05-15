namespace Application.Common.Exceptions
{
    public class VoteException : Exception 
    {
        public VoteException()
        {

        }

        public VoteException(string message) : base(message)
        {

        }

        public VoteException(string message, Exception exception) : base(message, exception)
        {

        }

        public VoteException(string name, object key) : base($"Entity \"{name}\" ({key}) could not be authenticated.")
        {

        }
    }
}
