namespace IdentityService.Application.Features.Groups.Queries.GetGroups;

/// <summary>
/// Represents the response object containing group details.
/// </summary>
public class GetGroupsQueryResponse
{
    /// <summary>
    /// Gets or sets the identifier of the group.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the group.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the path of the group.
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Gets or sets subgroups of the current group.
    /// </summary>
    public List<GetGroupsQueryResponse> SubGroups { get; set; }
}
