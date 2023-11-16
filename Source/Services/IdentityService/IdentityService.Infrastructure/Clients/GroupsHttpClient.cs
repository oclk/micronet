using IdentityService.Application.Common.Interfaces.Clients;
using IdentityService.Application.Common.Models.Clients.Groups;
using Shared.Extensions;

namespace IdentityService.Infrastructure.Clients;

/// <summary>
/// Implementation of the <see cref="IGroupsHttpClient"/> interface for interacting with the Groups service.
/// This class utilizes the provided <see cref="IHttpClientFactory"/> to create an HTTP client for the "groups" endpoint.
/// </summary>
public class GroupsHttpClient : IGroupsHttpClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroupsHttpClient"/> class with the specified <see cref="IHttpClientFactory"/>.
    /// </summary>
    /// <param name="httpClientFactory">The factory for creating HTTP clients.</param>
    public GroupsHttpClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    /// <summary>
    /// Retrieves the count of groups based on the provided request parameters.
    /// </summary>
    /// <param name="request">The request containing parameters for obtaining group count.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result contains the response with the group count.</returns>
    public async Task<GetGroupsCountResponse> GetGroupsCount(GetGroupsCountRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = _httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            GetGroupsCountResponse response = await httpClient.SendRESTRequestAsync<GetGroupsCountRequest, GetGroupsCountResponse>(HttpMethod.Get, $"{request.Realm}/groups/count", request, headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);

            // Return the obtained response containing the group count.
            return response;
        }
    }

    public async Task<List<GetGroupsResponse>> GetGroups(GetGroupsRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = _httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            List<GetGroupsResponse> response = await httpClient.SendRESTRequestAsync<GetGroupsRequest, List<GetGroupsResponse>>(HttpMethod.Get, $"{request.Realm}/groups", request, headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);

            // Return the obtained response containing the group count.
            return response;
        }
    }
}
