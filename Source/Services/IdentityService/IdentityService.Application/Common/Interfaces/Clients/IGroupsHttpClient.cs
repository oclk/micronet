namespace IdentityService.Application.Common.Interfaces.Clients;

#region Groups Http Client Models
// DeleteGroup Model(s)
public record DeleteGroupRequest(string Realm, string Id);

// GetGroup Model(s)
public record GetGroupRequest(string Realm, string Id);

// GetGroupManagementPermissions Model(s)
public record GetGroupManagementPermissionsRequest(string Realm, string Id);

// GetGroupMembers Model(s)
public record GetGroupMembersRequest(string Realm, string Id);
public record GetGroupMembersResponse();

// GetGroups Model(s)
public record GetGroupsRequest(string Realm, string Id);
public record GetGroupsResponse(string Id, string Name, string Path, List<GetGroupsResponse> SubGroups);

// GetGroupsCount Model(s)
public record GetGroupsCountRequest(string Realm);
public record UpdateGroupManagementPermissions(long Count);

// SetOrCreateSubGroup Model(s)
public record SetOrCreateSubGroupRequest(string Realm, string Id);

// UpdateGroup Model(s)
public record UpdateGroupRequest(string Realm, string Id, GroupRepresentation GroupRepresentation);

// UpdateGroupManagementPermissions Model(s)
public record UpdateGroupManagementPermissionsRequest(string Realm, string Id, GroupManagementPermissions GroupManagementPermissions);

// Common Model(s)
public record GroupRepresentation(string Id, string Name, string Path, Dictionary<string, string> Attributes, List<string> RealmRoles, Dictionary<string, string> ClientRoles, List<GroupRepresentation> SubGroups, List<bool> Access);
public record GroupManagementPermissions(bool Enabled, string Resource, Dictionary<string, string> ScopePermissions);
#endregion

public interface IGroupsHttpClient
{
    Task DeleteGroup(DeleteGroupRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);
    Task<GroupRepresentation> GetGroup(GetGroupRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);
    Task<GroupManagementPermissions> GetGroupManagementPermissions(GetGroupManagementPermissionsRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);
    Task<GetGroupMembersResponse> GetGroupMembers(GetGroupMembersRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);
    Task<List<GetGroupsResponse>> GetGroups(GetGroupsRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);
    Task<UpdateGroupManagementPermissions> GetGroupsCount(GetGroupsCountRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);
    Task<GroupRepresentation> SetOrCreateSubGroup(SetOrCreateSubGroupRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);
    Task<GroupRepresentation> UpdateGroup(UpdateGroupRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);
    Task<GroupManagementPermissions> UpdateGroupManagementPermissions(UpdateGroupManagementPermissionsRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);
}
