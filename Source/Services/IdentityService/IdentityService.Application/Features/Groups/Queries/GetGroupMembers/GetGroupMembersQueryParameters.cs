namespace IdentityService.Application.Features.Groups.Queries.GetGroupMembers;

/// <summary>
/// Represents the parameters used to query group members in the GetGroupMembersQuery.
/// </summary>
public class GetGroupMembersQueryParameters
{
    /// <summary>
    /// Gets or sets the parameter for obtaining a brief representation of group members.
    /// </summary>
    public string BriefRepresentation { get; set; }

    /// <summary>
    /// Gets or sets the parameter specifying the maximum number of group members to retrieve.
    /// </summary>
    public string Max { get; set; }

    /// <summary>
    /// Gets or sets the parameter for retrieving the first n group members.
    /// </summary>
    public string First { get; set; }
}
