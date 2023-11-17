using AutoMapper;
using IdentityService.Application.Common.Interfaces.Clients;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace IdentityService.Application.Features.Groups.Queries.GetGroups;

/// <summary>
/// Handles the MediatR query to retrieve group details.
/// </summary>
/// <param name="httpClientHelper">An auxiliary class for handling HTTP requests.</param>
/// <param name="configuration">Represents the application configuration.</param>
/// <param name="httpContextAccessor">An accessor for the HTTP context.</param>
public class GetGroupsQueryHandler(IGroupsHttpClient groupsHttpClient, IHttpContextAccessor httpContextAccessor, IMapper mapper) : IRequestHandler<GetGroupsQuery, List<GetGroupsQueryVm>>
{
    /// <summary>
    /// Processes the given query, retrieves the groups, and returns the response.
    /// </summary>
    /// <param name="request">The query to be processed.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The response containing the group details.</returns>
    public async Task<List<GetGroupsQueryVm>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
    {
        #region Setup Params
        // Get or create JWT
        string jwtToken = httpContextAccessor.HttpContext.Request.Headers.Authorization.ToString();

        // Generate Headers
        Dictionary<string, string> headers = new()
        {
            { "Authorization", jwtToken }
        };
        #endregion

        // Get & Return Response
        GetGroupsRequest getGroupsRequest = mapper.Map<GetGroupsRequest>(request);
        List<GetGroupsResponse> getGroupsResponse = await groupsHttpClient.GetGroups(getGroupsRequest, headers, request.QueryParameters, cancellationToken);
        List<GetGroupsQueryVm> response = mapper.Map<List<GetGroupsQueryVm>>(getGroupsResponse);

        return response;
    }
}
