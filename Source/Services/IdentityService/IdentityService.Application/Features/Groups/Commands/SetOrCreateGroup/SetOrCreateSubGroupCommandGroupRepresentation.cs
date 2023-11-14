namespace IdentityService.Application.Features.Groups.Commands.SetOrCreateGroup;

/// <summary>
/// Represents the data structure for the response of setting or creating a sub-group within a specified group.
/// </summary>
public class SetOrCreateSubGroupCommandGroupRepresentation
{
    /// <summary>
    /// Gets or sets the identifier of the sub-group.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the sub-group.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the path of the sub-group.
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Gets or sets the attributes associated with the sub-group.
    /// </summary>
    public Dictionary<string, string> Attributes { get; set; }

    /// <summary>
    /// Gets or sets the realm roles assigned to the sub-group.
    /// </summary>
    public List<string> RealmRoles { get; set; }

    /// <summary>
    /// Gets or sets the client roles assigned to the sub-group.
    /// </summary>
    public Dictionary<string, string> ClientRoles { get; set; }

    /// <summary>
    /// Gets or sets the list of sub-groups under the current sub-group.
    /// </summary>
    public List<SetOrCreateSubGroupCommandGroupRepresentation> SubGroups { get; set; }

    /// <summary>
    /// Gets or sets the list of access permissions for the sub-group.
    /// </summary>
    public List<bool> Access { get; set; }
}
