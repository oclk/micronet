using MediatR;

namespace IdentityService.Application.Features.Groups.Queries.GetGroups;

/// <summary>
/// Represents a query to retrieve groups with optional filtering parameters.
/// </summary>
public class GetGroupsQuery : IRequest<List<GetGroupsQueryVm>>
{
    /// <summary>
    /// Gets or sets the realm from which to retrieve the groups.
    /// </summary>
    public string Realm { get; set; }

    /// <summary>
    /// Gets or sets parameters to filter the groups.
    /// </summary>
    public Dictionary<string, string> QueryParameters { get; set; }
}

