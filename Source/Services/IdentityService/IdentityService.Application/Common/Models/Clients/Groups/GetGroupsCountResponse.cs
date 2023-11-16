namespace IdentityService.Application.Common.Models.Clients.Groups;

/// <summary>
/// Represents a response model containing the count of groups.
/// </summary>
public class GetGroupsCountResponse
{
    /// <summary>
    /// Gets or sets the count of groups.
    /// </summary>
    public long Count { get; set; }
}
