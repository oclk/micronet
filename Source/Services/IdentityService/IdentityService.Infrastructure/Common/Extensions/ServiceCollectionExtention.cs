using IdentityService.Application.Common.Interfaces.Clients;
using IdentityService.Infrastructure.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Infrastructure.Common.Extensions;

public static class ServiceCollectionExtention
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        #region Clients
        services.AddScoped<IGroupsHttpClient, GroupsHttpClient>();
        #endregion

        return services;
    }
}
