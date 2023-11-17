using Asp.Versioning;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.Configurations;
using Shared.Filters;
using Shared.Handlers;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IdentityService.Api.Common.Extensions;

/// <summary>
/// Extension methods for adding presentation services to the service collection.
/// </summary>
public static class ServiceCollectionExtention
{
    /// <summary>
    /// Adds presentation-related services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to which services are added.</param>
    /// <param name="configuration">The configuration providing access to application settings.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        #region General Configuration(s)
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddAutoMapper(typeof(Program));
#pragma warning disable CS0618 // Type or member is obsolete
        services.AddFluentValidation(fv =>
        {
            // You can make configurations for FluentValidation here.
            // For example, finding validators automatically:
            // fv.RegisterValidatorsFromAssemblyContaining<YourValidator>();
            fv.RegisterValidatorsFromAssemblyContaining<Program>();
        });
#pragma warning restore CS0618 // Type or member is obsolete
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        #endregion

        #region Api Versioning Configuration(s)
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                            new QueryStringApiVersionReader("x-api-version"),
                                                            new HeaderApiVersionReader("x-api-version"),
                                                            new MediaTypeApiVersionReader("x-api-version"));
        }).AddApiExplorer(options =>
        {
            // Add the versioned API explorer, which also adds IApiVersionDescriptionProvider service
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            options.GroupNameFormat = "'v'VVV";

            // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
            // can also be used to control the format of the API version in route templates
            options.SubstituteApiVersionInUrl = true;
        });
        #endregion

        #region Swagger Configuration
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "basic",
                In = ParameterLocation.Header,
                Description = "Basic Authorization header using the Bearer scheme."
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                   new OpenApiSecurityScheme
                   {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                   },
                   Array.Empty<string>()
                }
            });
            options.CustomSchemaIds(type => type.ToString());
            options.CustomSchemaIds(type => type.FullName);
            options.OperationFilter<SwaggerDefaultValuesFilter>();
        });
        #endregion

        #region Keycloak Auth Configuration
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var sslRequired = string.IsNullOrWhiteSpace(configuration["Keycloak:SslRequired"])
                    || configuration["Keycloak:SslRequired"]
                        .Equals("external", StringComparison.OrdinalIgnoreCase);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                options.Authority = $"{configuration["Keycloak:RealmUrl"]}/realms/transportex/";
                options.Audience = configuration["Keycloak:Client"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudiences = new string[] { "account", "transportex", "service-account" }
                };
                options.RequireHttpsMetadata = sslRequired;
                options.SaveToken = true;
                //options.Events = new JwtBearerEvents()
                //{
                //    OnAuthenticationFailed = c =>
                //    {
                //        c.NoResult();
                //        c.Response.StatusCode = 500;
                //        c.Response.ContentType = "text/plain";
                //        return c.Response.WriteAsync(c.Exception.ToString());
                //    }
                //};
                options.Validate();
            });
        #endregion

        #region CORS Configuration
        string[] _allowedOrigins = Array.Empty<string>();

        if (!string.IsNullOrEmpty(configuration["AllowedOrigin"]))
        {
            _allowedOrigins = configuration["AllowedOrigin"].Split(',', StringSplitOptions.RemoveEmptyEntries);
        }

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins(_allowedOrigins)
                            .AllowAnyHeader()
                            .WithMethods("GET", "PUT", "POST", "DELETE", "UPDATE", "OPTIONS")
                            .AllowCredentials();
                });
        });
        #endregion

        return services;
    }
}
