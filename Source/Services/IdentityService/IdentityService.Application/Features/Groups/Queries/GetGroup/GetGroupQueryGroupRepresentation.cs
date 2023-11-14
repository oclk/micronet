namespace IdentityService.Application.Features.Groups.Queries.GetGroup;

/// <summary>
/// Represents the data structure for retrieving information about a specific group.
/// </summary>
public class GetGroupQueryGroupRepresentation
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
    /// Gets or sets the attributes associated with the group.
    /// </summary>
    public Dictionary<string, string> Attributes { get; set; }

    /// <summary>
    /// Gets or sets the realm roles assigned to the group.
    /// </summary>
    public List<string> RealmRoles { get; set; }

    /// <summary>
    /// Gets or sets the client roles assigned to the group.
    /// </summary>
    public Dictionary<string, string> ClientRoles { get; set; }

    /// <summary>
    /// Gets or sets the list of sub-groups under the current group.
    /// </summary>
    public List<GetGroupQueryGroupRepresentation> SubGroups { get; set; }

    /// <summary>
    /// Gets or sets the list of access permissions for the group.
    /// </summary>
    public List<bool> Access { get; set; }
}
