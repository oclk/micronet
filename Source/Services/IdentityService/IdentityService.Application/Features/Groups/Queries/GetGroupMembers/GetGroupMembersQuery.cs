using MediatR;

namespace IdentityService.Application.Features.Groups.Queries.GetGroupMembers;

/// <summary>
/// Represents the request to query the members of a group using GetGroupMembersQuery.
/// </summary>
public class GetGroupMembersQuery : IRequest<GetGroupMembersQueryVm>
{
    /// <summary>
    /// Parameter representing the real-world identity of the group.
    /// </summary>
    public string Realm { get; set; }

    /// <summary>
    /// Parameter representing the unique identifier of the group.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Object containing custom parameters used to query group members.
    /// </summary>
    public Dictionary<string, string> QueryParameters { get; set; }
}
