using IdentityService.Application.Features.Groups.Queries.GetGroupsCount;

namespace IdentityService.Application.Common.Models.Clients.Groups;

/// <summary>
/// Represents a request object for retrieving groups, including the realm and query parameters.
/// </summary>
public class GetGroupsRequest
{
    /// <summary>
    /// Gets or sets the realm associated with the groups.
    /// </summary>
    public string Realm { get; set; }

    /// <summary>
    /// Gets or sets the query parameters for the request, including filters, sorting, and pagination.
    /// </summary>
    public GetGroupsCountQueryParameters QueryParameters { get; set; }
}
