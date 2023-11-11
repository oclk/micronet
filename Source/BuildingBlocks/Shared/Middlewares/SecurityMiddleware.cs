using Microsoft.AspNetCore.Http;

namespace Shared.Middlewares;

public class SecurityMiddleware
{
    private readonly RequestDelegate next;

    public SecurityMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // For Clickjacking, XSS ve MIME-type sniffing attacks
        context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");

        // For Clickjacking, XSS ve MIME-type sniffing attacks
        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

        // Disable to get pages in iframe for attackers
        context.Response.Headers.Add("X-Frame-Options", "DENY");

        // Disable the resource info for user
        context.Response.Headers.Add("Referrer-Policy", "no-referrer");

        // Indicate that we will not use them
        context.Response.Headers.Add("Feature-Policy",
                                     "camera 'none'; " +
                                     "accelerometer 'none'; " +
                                     "geolocation 'none'; " +
                                     "magnetometer 'none'; " +
                                     "microphone 'none'; " +
                                     "usb 'none'");

        // Change server name for success calls
        context.Response.Headers.SetCommaSeparatedValues("Server", "N/A");

        // Cant bury the page in adobe reader or some else with this
        context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");

        await next.Invoke(context);
    }
}
