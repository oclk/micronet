using IdentityService.Application.Common.Interfaces.Clients;
using Shared.Extensions;

namespace IdentityService.Infrastructure.Clients;

public class GroupsHttpClient(IHttpClientFactory httpClientFactory) : IGroupsHttpClient
{
    public async Task DeleteGroup(DeleteGroupRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            await httpClient.SendRESTRequestAsync<DeleteGroupRequest, object>(HttpMethod.Delete, $"{request.Realm}/groups/{request.Id}", request, headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);
        }
    }

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

    public async Task<GroupManagementPermissions> GetGroupManagementPermissions(GetGroupManagementPermissionsRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            GroupManagementPermissions response = await httpClient.SendRESTRequestAsync<GetGroupManagementPermissionsRequest, GroupManagementPermissions>(HttpMethod.Get, $"{request.Realm}/groups/{request.Id}/management/permissions", request, headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);

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

    public async Task<UpdateGroupManagementPermissions> GetGroupsCount(GetGroupsCountRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            UpdateGroupManagementPermissions response = await httpClient.SendRESTRequestAsync<GetGroupsCountRequest, UpdateGroupManagementPermissions>(HttpMethod.Get, $"{request.Realm}/groups/count", request, headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);

            // Return the obtained response containing the group count.
            return response;
        }
    }

    public async Task<GroupRepresentation> SetOrCreateSubGroup(SetOrCreateSubGroupRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            GroupRepresentation response = await httpClient.SendRESTRequestAsync<SetOrCreateSubGroupRequest, GroupRepresentation>(HttpMethod.Post, $"{request.Realm}/groups/{request.Id}/children", request, headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);

            // Return the obtained response containing the group count.
            return response;
        }
    }

    public async Task<GroupRepresentation> UpdateGroup(UpdateGroupRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            GroupRepresentation response = await httpClient.SendRESTRequestAsync<UpdateGroupRequest, GroupRepresentation>(HttpMethod.Put, $"{request.Realm}/groups/{request.Id}", request, headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);

            // Return the obtained response containing the group count.
            return response;
        }
    }

    public async Task<GroupManagementPermissions> UpdateGroupManagementPermissions(UpdateGroupManagementPermissionsRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            GroupManagementPermissions response = await httpClient.SendRESTRequestAsync<UpdateGroupManagementPermissionsRequest, GroupManagementPermissions>(HttpMethod.Put, $"{request.Realm}/groups/{request.Id}/management/permissions", request, headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);

            // Return the obtained response containing the group count.
            return response;
        }
    }
}
