namespace IdentityService.Application.Features.Groups.Queries.GetGroups;

/// <summary>
/// Represents query parameters used to filter the group details.
/// </summary>
public class GetGroupsQueryParameters
{
    /// <summary>
    /// Gets or sets a boolean property indicating whether to use brief representation.
    /// </summary>
    public bool BriefRepresentation { get; set; }

    /// <summary>
    /// Gets or sets a boolean property indicating whether to perform an exact match.
    /// </summary>
    public bool Exact { get; set; }

    /// <summary>
    /// Gets or sets the first result to retrieve.
    /// </summary>
    public string First { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of results to retrieve.
    /// </summary>
    public string Max { get; set; }

    /// <summary>
    /// Gets or sets a boolean property indicating whether to populate the hierarchy.
    /// </summary>
    public bool PopulateHierarchy { get; set; }

    /// <summary>
    /// Gets or sets the search query.
    /// </summary>
    public string Q { get; set; }

    /// <summary>
    /// Gets or sets the search string.
    /// </summary>
    public string Search { get; set; }
}
