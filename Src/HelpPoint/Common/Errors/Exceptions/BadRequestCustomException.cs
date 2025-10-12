namespace HelpPoint.Common.Errors.Exceptions;

public class BadRequestCustomException(string message) : ServiceException(StatusCodes.Status400BadRequest, message);

