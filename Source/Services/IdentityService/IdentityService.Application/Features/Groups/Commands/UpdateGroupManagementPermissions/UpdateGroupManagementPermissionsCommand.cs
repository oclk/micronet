using MediatR;

namespace IdentityService.Application.Features.Groups.Commands.UpdateGroupManagementPermissions;

/// <summary>
/// Represents a command to update the management permissions associated with a specific group.
/// </summary>
public class UpdateGroupManagementPermissionsCommand : IRequest<UpdateGroupManagementPermissionsCommandDto>
{
    /// <summary>
    /// Gets or sets the realm of the group for which management permissions will be updated.
    /// </summary>
    public string Realm { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the group for which management permissions will be updated.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the reference for the management permissions to be updated.
    /// </summary>
    public UpdateGroupManagementPermissionsCommandDto GroupManagementPermissions { get; set; }
}
