namespace HelpPoint.Common.Errors.Exceptions;

public class ServiceException(int statusCode, string message) : Exception
{
    public int StatusCode { get; } = statusCode;
    public string ErrorMessage { get; } = message;
}
