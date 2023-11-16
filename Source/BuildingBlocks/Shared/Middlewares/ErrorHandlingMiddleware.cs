using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Constants;
using Shared.Models;
using System.Net;

namespace Shared.Middlewares;

/// <summary>
/// Middleware for handling errors during request processing and providing detailed logging and appropriate error responses.
/// </summary>
/// <param name="next"></param>
/// <param name="logger"></param>
public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
{
    /// <summary>
    /// Invokes the middleware to handle errors during request processing.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            // Detailed logging
            logger.LogError(ex, "An error occurred during request processing.");

            // Determining Status Code according to error status
            HttpStatusCode statusCode = GetStatusCode(ex);

            // Prepare error response
            Result<object> response = new() { Message = ex.Message };
            string responseJsonStr = JsonConvert.SerializeObject(response);

            // Set response
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = ContentTypes.APPLICATION_JSON;

            // Write the response
            await context.Response.WriteAsync(responseJsonStr);
        }
    }

    /// <summary>
    /// Determines the HTTP status code according to the type of error.
    /// </summary>
    /// <param name="ex">The exception representing the error.</param>
    /// <returns>The corresponding HTTP status code.</returns>
    private HttpStatusCode GetStatusCode(Exception ex)
    {
        // Determining Status Code according to error
        if (ex is ArgumentNullException) return HttpStatusCode.BadRequest;
        else if (ex is ArgumentException) return HttpStatusCode.BadRequest;
        else if (ex is ApplicationException) return HttpStatusCode.BadRequest;
        else if (ex is UnauthorizedAccessException) return HttpStatusCode.Unauthorized;
        else
        {
            // For other errors you can use Internal Server Error as default
            return HttpStatusCode.InternalServerError;
        }
    }
}
