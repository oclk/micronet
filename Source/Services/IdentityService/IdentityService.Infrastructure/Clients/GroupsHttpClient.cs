using IdentityService.Application.Common.Interfaces.Clients;
using Shared.Extensions;

namespace IdentityService.Infrastructure.Clients;

/// <summary>
/// HTTP client implementation for interacting with group-related operations.
/// </summary>
public class GroupsHttpClient(IHttpClientFactory httpClientFactory) : IGroupsHttpClient
{
    /// <summary>
    /// Creates a new group with the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for creating a group.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The representation of the newly created group.</returns>
    public async Task<GroupRepresentation> CreateGroup(CreateGroupRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            GroupRepresentation response = await httpClient.SendRESTRequestAsync<GroupRepresentation, GroupRepresentation>(HttpMethod.Post, $"{request.Realm}/groups", request.GroupRepresentation, headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);

            // Return the obtained response containing the group count.
            return response;
        }
    }

    /// <summary>
    /// Deletes the group identified by the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for deleting a group.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    public async Task DeleteGroup(DeleteGroupRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            await httpClient.SendRESTRequestAsync<object, object>(HttpMethod.Delete, $"{request.Realm}/groups/{request.Id}", headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);
        }
    }

    /// <summary>
    /// Retrieves the details of the group identified by the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for retrieving a group.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The representation of the retrieved group.</returns>
    public async Task<GroupRepresentation> GetGroup(GetGroupRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            GroupRepresentation response = await httpClient.SendRESTRequestAsync<GetGroupRequest, GroupRepresentation>(HttpMethod.Get, $"{request.Realm}/groups/{request.Id}", request, headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);

            // Return the obtained response containing the group count.
            return response;
        }
    }

    /// <summary>
    /// Retrieves the management permissions of the group identified by the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for retrieving group management permissions.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The group management permissions.</returns>
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

    /// <summary>
    /// Retrieves the members of the group identified by the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for retrieving group members.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The response containing group members.</returns>
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

    /// <summary>
    /// Retrieves the list of groups based on the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for retrieving groups.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The list of groups.</returns>
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

    /// <summary>
    /// Retrieves the count of groups based on the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for retrieving the count of groups.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The count of groups.</returns>
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

    /// <summary>
    /// Sets or creates a sub-group based on the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for setting or creating a sub-group.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The representation of the set or created sub-group.</returns>
    public async Task<GroupRepresentation> SetOrCreateSubGroup(SetOrCreateSubGroupRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            GroupRepresentation response = await httpClient.SendRESTRequestAsync<GroupRepresentation, GroupRepresentation>(HttpMethod.Post, $"{request.Realm}/groups/{request.Id}/children", request.GroupRepresentation, headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);

            // Return the obtained response containing the group count.
            return response;
        }
    }

    /// <summary>
    /// Updates the group based on the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for updating a group.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The representation of the updated group.</returns>
    public async Task<GroupRepresentation> UpdateGroup(UpdateGroupRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            GroupRepresentation response = await httpClient.SendRESTRequestAsync<GroupRepresentation, GroupRepresentation>(HttpMethod.Put, $"{request.Realm}/groups/{request.Id}", request.GroupRepresentation, headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);

            // Return the obtained response containing the group count.
            return response;
        }
    }

    /// <summary>
    /// Updates the group management permissions based on the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for updating group management permissions.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The updated group management permissions.</returns>
    public async Task<GroupManagementPermissions> UpdateGroupManagementPermissions(UpdateGroupManagementPermissionsRequest request, Dictionary<string, string>? headers = null, Dictionary<string, string>? queryParameters = null, CancellationToken cancellationToken = default)
    {
        // Create an HTTP client specifically configured for the "groups" endpoint.
        using (var httpClient = httpClientFactory.CreateClient("admin"))
        {
            // Send a RESTful request to the endpoint using the provided request parameters.
            GroupManagementPermissions response = await httpClient.SendRESTRequestAsync<GroupManagementPermissions, GroupManagementPermissions>(HttpMethod.Put, $"{request.Realm}/groups/{request.Id}/management/permissions", request.GroupManagementPermissions, headers: headers, queryParameters: queryParameters, cancellationToken: cancellationToken);

            // Return the obtained response containing the group count.
            return response;
        }
    }
}
