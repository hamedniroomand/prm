namespace PMS.Contracts.Exceptions;

public class AppException : Exception
{
    public AppException(string message) : base(message) { }
}

public class NotFoundException : AppException
{
    public NotFoundException(string message = "Not Found") : base(message)
    {
    }
}