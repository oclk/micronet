using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shared.Configurations;

/// <summary>
/// Configuration options for Swagger generation based on API versions.
/// </summary>
/// <param name="provider"></param>
public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    /// <summary>
    /// Configures Swagger generation options for each API version.
    /// </summary>
    /// <param name="options">The Swagger generation options to configure.</param>
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }

    /// <summary>
    /// Creates Swagger information for a specific API version.
    /// </summary>
    /// <param name="description">The API version description.</param>
    /// <returns>OpenApiInfo for the specified API version.</returns>
    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "IdentityService Api",
            Version = description.ApiVersion.ToString(),
            Description = "The IdentityService API manages user identities, providing endpoints for registration, login, and identity operations.",
            Contact = new OpenApiContact { Name = "Ömer Çelik", Email = "oclk@outlook.com.tr" },
            License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}
