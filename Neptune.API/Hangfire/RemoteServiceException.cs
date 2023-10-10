using System;

namespace Neptune.API.Hangfire;

public class RemoteServiceException : Exception
{
    public RemoteServiceException(string message) : base(message)
    {

    }

    public RemoteServiceException(string message, Exception innerException) : base(message, innerException)
    {

    }
}