using MediatR;

namespace IdentityService.Application.Features.Groups.Queries.GetGroupsCount;

/// <summary>
/// Represents a query to retrieve the count of groups with optional filtering parameters.
/// </summary>
public class GetGroupsCountQuery : IRequest<GetGroupsCountQueryResponse>
{
    /// <summary>
    /// Gets or sets the realm from which to retrieve the group count.
    /// </summary>
    public string Realm { get; set; }

    /// <summary>
    /// Gets or sets parameters to filter the group count.
    /// </summary>
    public GetGroupsCountQueryParameters QueryParameters { get; set; }
}
