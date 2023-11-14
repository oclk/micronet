namespace IdentityService.Application.Features.Groups.Queries.GetGroupManagementPermissions;

/// <summary>
/// Represents a reference for the management permissions associated with a group in the context of the GetGroupManagementPermissions query.
/// </summary>
public class GetGroupManagementPermissionsQueryManagementPermissionReference
{
    /// <summary>
    /// Gets or sets a value indicating whether the management permission is enabled or not.
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
