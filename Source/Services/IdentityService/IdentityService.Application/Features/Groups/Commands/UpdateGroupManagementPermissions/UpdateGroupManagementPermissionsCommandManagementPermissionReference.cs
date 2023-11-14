namespace IdentityService.Application.Features.Groups.Commands.UpdateGroupManagementPermissions;

/// <summary>
/// Represents a reference for the management permissions to be updated in the context of the UpdateGroupManagementPermissionsCommand.
/// </summary>
public class UpdateGroupManagementPermissionsCommandManagementPermissionReference
{
    /// <summary>
    /// Gets or sets a value indicating whether the management permission should be enabled or not.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the resource associated with the management permission.
    /// </summary>
    public string Resource { get; set; }

    /// <summary>
    /// Gets or sets the scope permissions associated with the management permission.
    /// </summary>
    public Dictionary<string, string> ScopePermissions { get; set; }
}
