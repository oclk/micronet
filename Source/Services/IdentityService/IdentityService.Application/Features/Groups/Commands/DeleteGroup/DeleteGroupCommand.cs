using MediatR;

namespace IdentityService.Application.Features.Groups.Commands.DeleteGroup;

/// <summary>
/// Represents a command to delete a group and its sub-groups.
/// </summary>
public class DeleteGroupCommand : IRequest
{
    /// <summary>
    /// Gets or sets the realm to which the group belongs.
    /// </summary>
    public string Realm { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the group to be deleted.
    /// </summary>
    public string Id { get; set; }
}
