namespace IdentityService.Application.Common.Interfaces.Clients;

#region Groups Http Client Models
// DeleteGroup Model(s)
public record DeleteGroupRequest(string Realm, string Id);

// GetGroup Model(s)
public record GetGroupRequest(string Realm, string Id);

// GetGroupManagementPermissions Model(s)
public record GetGroupManagementPermissionsRequest(string Realm, string Id);
public record GetGroupManagementPermissionsResponse(bool Enabled, string Resource, Dictionary<string, string> ScopePermissions);

// GetGroupMembers Model(s)
public record GetGroupMembersRequest(string Realm, string Id);
public record GetGroupMembersResponse();

// GetGroups Model(s)
public record GetGroupsRequest(string Realm, string Id);
public record GetGroupsResponse(string Id, string Name, string Path, List<GetGroupsResponse> SubGroups);

// GetGroupsCount Model(s)
public record GetGroupsCountRequest(string Realm);
public record GetGroupsCountResponse(long Count);

// Common Model(s)
public record GroupRepresentation(string Id, string Name, string Path, Dictionary<string, string> Attributes, List<string> RealmRoles, Dictionary<string, string> ClientRoles, List<GroupRepresentation> SubGroups, List<bool> Access);
#endregion

public interface IGroupsHttpClient
{
    Task DeleteGroup(DeleteGroupRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);

    Task<GroupRepresentation> GetGroup(GetGroupRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);
    Task<GetGroupManagementPermissionsResponse> GetGroupManagementPermissions(GetGroupManagementPermissionsRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);
    Task<GetGroupMembersResponse> GetGroupMembers(GetGroupMembersRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);
    Task<List<GetGroupsResponse>> GetGroups(GetGroupsRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);
    Task<GetGroupsCountResponse> GetGroupsCount(GetGroupsCountRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);
}
