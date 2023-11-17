using MediatR;

namespace IdentityService.Application.Features.Groups.Commands.CreateGroup;

/// <summary>
/// Represents a command to create a group
/// </summary>
public class CreateGroupCommand :IRequest<CreateGroupCommandDto>
{
    /// <summary>
    /// Gets or sets the real-world identity of the group.
    /// </summary>
    public string Realm { get; set; }

    /// <summary>
    /// Gets or sets the representation of the updated group details.
    /// </summary>
    public CreateGroupCommandDto GroupRepresentation { get; set; }
}
