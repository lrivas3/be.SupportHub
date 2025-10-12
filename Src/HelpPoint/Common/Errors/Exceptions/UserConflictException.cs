using System.Net;

namespace HelpPoint.Common.Errors.Exceptions;

public class UserConflictException(string message) :
    ServiceException(StatusCodes.Status409Conflict, message);
