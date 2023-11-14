namespace IdentityService.Application.Features.Groups.Queries.GetGroupsCount;

/// <summary>
/// Represents the response object containing the count of groups.
/// </summary>
public class GetGroupsCountQueryResponse
{
    /// <summary>
    /// Gets or sets the total count of groups.
    /// </summary>
    public long Count { get; set; }
}
