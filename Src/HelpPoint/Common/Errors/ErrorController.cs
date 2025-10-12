using HelpPoint.Common.Errors.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HelpPoint.Common.Errors;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController(ILogger<ErrorController> logger, IWebHostEnvironment env) : ControllerBase
{
    private IWebHostEnvironment Env { get; } = env;

    [Route("/error")]
    public IActionResult Errors()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is null)
        {
            logger.LogError("An unknown error occurred without an exception.");
            return Problem();
        }

        logger.LogError(exception, "An exception occurred: {Message}", exception.Message);

        return exception switch
        {
            ServiceException serviceException => Problem(statusCode: serviceException.StatusCode,
                detail: serviceException.ErrorMessage),

            HelpPointValidationException validationException => new ObjectResult(new ValidationProblemDetails
            {
                Title = "Validation failed",
                Status = validationException.StatusCode,
                Errors = validationException.Errors
            })
            { StatusCode = validationException.StatusCode },

            _ => Env.IsDevelopment() ? throw exception : Problem("An unexpected error occurred")
        };
    }
}
