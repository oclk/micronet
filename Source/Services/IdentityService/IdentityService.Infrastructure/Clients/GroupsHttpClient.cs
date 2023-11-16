using IdentityService.Application.Common.Interfaces.Clients;
using Shared.Extensions;

namespace IdentityService.Infrastructure.Clients;

public class GroupsHttpClient(IHttpClientFactory httpClientFactory) : IGroupsHttpClient
{
    public async Task<GroupRepresentation> GetGroup(GetGroupRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            GroupRepresentation response = await httpClient.SendRESTRequestAsync<GetGroupRequest, GroupRepresentation>(HttpMethod.Get, $"{request.Realm}/groups{request.Id}", request, headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);

            // Return the obtained response containing the group count.
            return response;
        }
    }

    public async Task<GetGroupManagementPermissionsResponse> GetGroupManagementPermissions(GetGroupManagementPermissionsRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            GetGroupManagementPermissionsResponse response = await httpClient.SendRESTRequestAsync<GetGroupManagementPermissionsRequest, GetGroupManagementPermissionsResponse>(HttpMethod.Get, $"{request.Realm}/groups/{request.Id}/management/permissions", request, headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);

            // Return the obtained response containing the group count.
            return response;
        }
    }

    public async Task<GetGroupMembersResponse> GetGroupMembers(GetGroupMembersRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            GetGroupMembersResponse response = await httpClient.SendRESTRequestAsync<GetGroupMembersRequest, GetGroupMembersResponse>(HttpMethod.Get, $"{request.Realm}/groups{request.Id}/members", request, headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);

            // Return the obtained response containing the group count.
            return response;
        }
    }

    public async Task<List<GetGroupsResponse>> GetGroups(GetGroupsRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            List<GetGroupsResponse> response = await httpClient.SendRESTRequestAsync<GetGroupsRequest, List<GetGroupsResponse>>(HttpMethod.Get, $"{request.Realm}/groups", request, headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);

            // Return the obtained response containing the group count.
            return response;
        }
    }

    public async Task<GetGroupsCountResponse> GetGroupsCount(GetGroupsCountRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            GetGroupsCountResponse response = await httpClient.SendRESTRequestAsync<GetGroupsCountRequest, GetGroupsCountResponse>(HttpMethod.Get, $"{request.Realm}/groups/count", request, headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);

            // Return the obtained response containing the group count.
            return response;
        }
    }
}
