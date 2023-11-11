using Microsoft.AspNetCore.Http;
using System.Net;

namespace Shared.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // LogException(ex);

            // Special error pages can be shown depending on the error status
            if (ex is ApplicationException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Özel bir hata oluştu.");
            }
            else if (ex is UnauthorizedAccessException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Yetkisiz erişim.");
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/plain";
                await context.Response.WriteAsync("Bir hata oluştu.");
            }
        }
    }
}
