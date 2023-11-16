using MediatR;

namespace IdentityService.Application.Features.Groups.Queries.GetGroupManagementPermissions;

/// <summary>
/// Represents a query to retrieve management permissions associated with a specific group.
/// </summary>
public class GetGroupManagementPermissionsQuery : IRequest<GetGroupManagementPermissionsQueryVm>
{
    /// <summary>
    /// Gets or sets the realm of the group for which management permissions will be retrieved.
    /// </summary>
    public string Realm { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the group for which management permissions will be retrieved.
    /// </summary>
    public string Id { get; set; }
}
