using MediatR;

namespace IdentityService.Application.Features.Groups.Commands.UpdateGroup;

/// <summary>
/// Represents a command to update the details of a group.
/// </summary>
public class UpdateGroupCommand : IRequest<UpdateGroupCommandGroupRepresentation>
{
    /// <summary>
    /// Gets or sets the real-world identity of the group.
    /// </summary>
    public string Realm { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the group.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the representation of the updated group details.
    /// </summary>
    public UpdateGroupCommandGroupRepresentation GroupRepresentation { get; set; }
}
