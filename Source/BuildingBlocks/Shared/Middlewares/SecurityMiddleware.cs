using Microsoft.AspNetCore.Http;

namespace Shared.Middlewares;

public class SecurityMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        // For Clickjacking, XSS ve MIME-type sniffing attacks
        context.Response.Headers.TryAdd("X-Xss-Protection", "1; mode=block");

        // For Clickjacking, XSS ve MIME-type sniffing attacks
        context.Response.Headers.TryAdd("X-Content-Type-Options", "nosniff");

        // Disable to get pages in iframe for attackers
        context.Response.Headers.TryAdd("X-Frame-Options", "DENY");

        // Disable the resource info for user
        context.Response.Headers.TryAdd("Referrer-Policy", "no-referrer");

        // Indicate that we will not use them
        context.Response.Headers.TryAdd("Feature-Policy",
                                     "camera 'none'; " +
                                     "accelerometer 'none'; " +
                                     "geolocation 'none'; " +
                                     "magnetometer 'none'; " +
                                     "microphone 'none'; " +
                                     "usb 'none'");

        // Change server name for success calls
        context.Response.Headers.SetCommaSeparatedValues("Server", "N/A");

        // Cant bury the page in adobe reader or some else with this
        context.Response.Headers.TryAdd("X-Permitted-Cross-Domain-Policies", "none");

        await next.Invoke(context);
    }
}
