﻿namespace IdentityService.Application.Common.Interfaces.Clients;

#region Groups Http Client Models
/// <summary>
/// Represents a request to create a new group.
/// </summary>
public record CreateGroupRequest(string Realm, GroupRepresentation GroupRepresentation);

/// <summary>
/// Represents a request to delete a group.
/// </summary>
public record DeleteGroupRequest(string Realm, string Id);

/// <summary>
/// Represents a request to get information about a specific group.
/// </summary>
public record GetGroupRequest(string Realm, string Id);

/// <summary>
/// Represents a request to get the management permissions of a group.
/// </summary>
public record GetGroupManagementPermissionsRequest(string Realm, string Id);

/// <summary>
/// Represents a request to get the members of a group.
/// </summary>
public record GetGroupMembersRequest(string Realm, string Id);

/// <summary>
/// Represents the response for getting group members.
/// </summary>
public record GetGroupMembersResponse();

/// <summary>
/// Represents a request to get information about groups.
/// </summary>
public record GetGroupsRequest(string Realm);

/// <summary>
/// Represents the response for getting information about groups.
/// </summary>
public record GetGroupsResponse(string Id, string Name, string Path, List<GetGroupsResponse> SubGroups);

/// <summary>
/// Represents a request to get the count of groups.
/// </summary>
public record GetGroupsCountRequest(string Realm);

/// <summary>
/// Represents the response for getting the count of groups.
/// </summary>
public record GetGroupsCountResponse(long Count);

/// <summary>
/// Represents a request to set or create a sub-group.
/// </summary>
public record SetOrCreateSubGroupRequest(string Realm, string Id, GroupRepresentation GroupRepresentation);

/// <summary>
/// Represents a request to update information about a group.
/// </summary>
public record UpdateGroupRequest(string Realm, string Id, GroupRepresentation GroupRepresentation);

/// <summary>
/// Represents a request to update the management permissions of a group.
/// </summary>
public record UpdateGroupManagementPermissionsRequest(string Realm, string Id, GroupManagementPermissions GroupManagementPermissions);

/// <summary>
/// Represents the information about a group.
/// </summary>
public record GroupRepresentation(string Id, string Name, string Path, Dictionary<string, string> Attributes, List<string> RealmRoles, Dictionary<string, string> ClientRoles, List<GroupRepresentation> SubGroups, List<bool> Access);

/// <summary>
/// Represents the management permissions of a group.
/// </summary>
public record GroupManagementPermissions(bool Enabled, string Resource, Dictionary<string, string> ScopePermissions);
#endregion

/// <summary>
/// Interface for the Groups HTTP client, defining methods for various group-related operations.
/// </summary>
public interface IGroupsHttpClient
{
    /// <summary>
    /// Creates a new group with the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for creating a group.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The representation of the newly created group.</returns>
    Task<GroupRepresentation> CreateGroup(CreateGroupRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the group identified by the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for deleting a group.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    Task DeleteGroup(DeleteGroupRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the details of the group identified by the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for retrieving a group.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The representation of the retrieved group.</returns>
    Task<GroupRepresentation> GetGroup(GetGroupRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the management permissions of the group identified by the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for retrieving group management permissions.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The group management permissions.</returns>
    Task<GroupManagementPermissions> GetGroupManagementPermissions(GetGroupManagementPermissionsRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the members of the group identified by the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for retrieving group members.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The response containing group members.</returns>
    Task<GetGroupMembersResponse> GetGroupMembers(GetGroupMembersRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the list of groups based on the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for retrieving groups.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The list of groups.</returns>
    Task<List<GetGroupsResponse>> GetGroups(GetGroupsRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the count of groups based on the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for retrieving the count of groups.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The count of groups.</returns>
    Task<GetGroupsCountResponse> GetGroupsCount(GetGroupsCountRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets or creates a sub-group based on the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for setting or creating a sub-group.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The representation of the set or created sub-group.</returns>
    Task<GroupRepresentation> SetOrCreateSubGroup(SetOrCreateSubGroupRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the group based on the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for updating a group.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The representation of the updated group.</returns>
    Task<GroupRepresentation> UpdateGroup(UpdateGroupRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the group management permissions based on the specified request parameters.
    /// </summary>
    /// <param name="request">The request parameters for updating group management permissions.</param>
    /// <param name="headers">Optional headers for the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters for the HTTP request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The updated group management permissions.</returns>
    Task<GroupManagementPermissions> UpdateGroupManagementPermissions(UpdateGroupManagementPermissionsRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);
}
