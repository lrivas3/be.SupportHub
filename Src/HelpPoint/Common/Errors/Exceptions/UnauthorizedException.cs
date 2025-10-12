namespace HelpPoint.Common.Errors.Exceptions;

public class UnauthorizedException(string message) :
    ServiceException(StatusCodes.Status403Forbidden, message);
