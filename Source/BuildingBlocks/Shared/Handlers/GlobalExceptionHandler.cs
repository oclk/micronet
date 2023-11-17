using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Shared.Handlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        logger.LogError(
            exception, 
            "Could not process a request on machine {MachineName}. TraceId: {TraceId}",
            Environment.MachineName, 
            traceId
        );

        (int statusCode, string title, string detail) = MapException(exception);

        await Results.Problem(
            type: exception.GetType().Name,
            title: title,
            detail: detail,
            statusCode: statusCode,
            instance: $"{httpContext.Request.Method} {httpContext.Request.Path}",
            extensions: new Dictionary<string, object>
            {
                { "traceId", traceId }
            }
        ).ExecuteAsync(httpContext);

        return true;
    }

    private static (int statusCode,  string title, string detail) MapException(Exception exception)
    {
        return exception switch
        {
            ArgumentNullException => (StatusCodes.Status400BadRequest, exception.Message, exception.StackTrace),
            ArgumentException => (StatusCodes.Status400BadRequest, exception.Message, exception.StackTrace),
            ApplicationException => (StatusCodes.Status400BadRequest, exception.Message, exception.StackTrace),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, exception.Message, exception.StackTrace),
            _ => (StatusCodes.Status500InternalServerError, exception.Message, exception.StackTrace)
        };
    }
}
