using MediatR;

namespace IdentityService.Application.Features.Groups.Commands.SetOrCreateGroup;

/// <summary>
/// Represents a command to set or create a sub-group within a specified group.
/// </summary>
public class SetOrCreateSubGroupCommand : IRequest<SetOrCreateSubGroupCommandGroupRepresentation>
{
    /// <summary>
    /// Gets or sets the realm to which the group belongs.
    /// </summary>
    public string Realm { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the parent group.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the representation of the sub-group to be set or created.
    /// </summary>
    public SetOrCreateSubGroupCommandGroupRepresentation SetOrCreateSubGroupCommandGroupRepresentation { get; set; }
}
