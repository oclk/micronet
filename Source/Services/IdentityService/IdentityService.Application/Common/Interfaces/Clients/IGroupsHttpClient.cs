using IdentityService.Application.Common.Models.Clients.Groups;

namespace IdentityService.Application.Common.Interfaces.Clients;

/// <summary>
/// Interface defining the contract for an HTTP client to interact with the Groups service.
/// </summary>
public interface IGroupsHttpClient
{
    /// <summary>
    /// Retrieves the count of groups based on the provided request parameters.
    /// </summary>
    /// <param name="request">The request containing parameters for obtaining group count.</param>
    /// <param name="headers">Optional headers to include in the HTTP request.</param>
    /// <param name="queryParameters">Optional query parameters to include in the HTTP request.</param>
    /// <param name="cancellationToken">Optional cancellation token for canceling the asynchronous operation.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result contains the response with the group count.</returns>
    Task<GetGroupsCountResponse> GetGroupsCount(GetGroupsCountRequest request, Dictionary<string, string> headers = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default);
}
