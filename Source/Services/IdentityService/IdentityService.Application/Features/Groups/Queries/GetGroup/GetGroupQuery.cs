using MediatR;

namespace IdentityService.Application.Features.Groups.Queries.GetGroup;

/// <summary>
/// Represents a query to retrieve information about a specific group.
/// </summary>
public class GetGroupQuery : IRequest<GetGroupQueryVm>
{
    /// <summary>
    /// Gets or sets the realm to which the group belongs.
    /// </summary>
    public string Realm { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the group to retrieve information about.
    /// </summary>
    public string Id { get; set; }
}
