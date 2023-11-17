using Asp.Versioning.ApiExplorer;
using Shared.Middlewares;

namespace IdentityService.Api.Common.Extensions;

/// <summary>
/// Extension methods for configuring and adding middleware to the ASP.NET Core WebApplication.
/// </summary>
public static class WebApplicationExtension
{
    /// <summary>
    /// Configures and adds middleware to the specified <see cref="WebApplication"/>.
    /// </summary>
    /// <param name="app">The WebApplication to which middleware is added.</param>
    /// <returns>The modified WebApplication.</returns>
    public static WebApplication Use(this WebApplication app)
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        #region General
        app.MapControllers();
        // app.UseHttpsRedirection();
        #endregion

        #region Swagger
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var descriptions = app.DescribeApiVersions();

                // Build a swagger endpoint for each discovered API version
                foreach (var description in descriptions)
                {
                    var url = $"/swagger/{description.GroupName}/swagger.json";
                    var name = description.GroupName.ToUpperInvariant();
                    options.SwaggerEndpoint(url, name);
                }
            });
        }
        #endregion

        #region Middleware(s)
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseMiddleware(typeof(SecurityMiddleware));
        #endregion

        #region Auth
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors();
        #endregion

        return app;
    }
}
