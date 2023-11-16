namespace IdentityService.Application.Common.Models.Clients.Groups;

/// <summary>
/// Represents a request model used to retrieve the count of groups.
/// </summary>
public class GetGroupsCountRequest
{
    /// <summary>
    /// Gets or sets the realm associated with the groups.
    /// </summary>
    public string Realm { get; set; }
}
