namespace IdentityService.Application.Features.Groups.Queries.GetGroupsCount;

/// <summary>
/// Represents query parameters used to filter the group count.
/// </summary>
public class GetGroupsCountQueryParameters
{
    /// <summary>
    /// Gets or sets the property for searching groups.
    /// </summary>
    public string Search { get; set; }

    /// <summary>
    /// Gets or sets a boolean property indicating whether to fetch a specific number of top groups.
    /// </summary>
    public bool Top { get; set; }
}
