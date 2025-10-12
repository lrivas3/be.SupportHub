namespace HelpPoint.Common.Errors.Exceptions;

public class NotFoundException(string message) :
    ServiceException(StatusCodes.Status404NotFound, message);
