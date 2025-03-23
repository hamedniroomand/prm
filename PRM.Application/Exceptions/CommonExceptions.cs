namespace PMS.Contracts.Exceptions;

public class AppException : Exception
{
    public AppException(string message) : base(message) { }
}

public class NotFoundException(string message = "Not Found") : AppException(message);
public class UnauthorizedException(string message = "Unauthorized") : AppException(message);