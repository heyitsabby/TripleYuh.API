﻿namespace Application.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() { }

        public UnauthorizedException(string message) : base(message) { }

        public UnauthorizedException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
