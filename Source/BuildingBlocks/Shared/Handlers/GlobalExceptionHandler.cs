using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Shared.Handlers;

/// <summary>
/// Handles global exceptions in the application and logs details for debugging purposes.
/// </summary>
/// <param name="logger"></param>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    /// <summary>
    /// Tries to handle an exception by logging details and returning an appropriate problem response.
    /// </summary>
    /// <param name="httpContext">The HTTP context of the current request.</param>
    /// <param name="exception">The exception to handle.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation, indicating whether the exception was handled.</returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        // Retrieve the trace identifier for the request
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        // Log exception details for debugging purposes
        logger.LogError(
            exception, 
            "Could not process a request on machine {MachineName}. TraceId: {TraceId}",
            Environment.MachineName, 
            traceId
        );

        // Map the exception to status code, title, and detail
        (int statusCode, string title, string detail) = MapException(exception);

        // Return a problem response with the mapped details
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

    /// <summary>
    /// Maps the exception to a status code, title, and detail.
    /// </summary>
    /// <param name="exception">The exception to map.</param>
    /// <returns>A tuple containing the status code, title, and detail.</returns>
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
