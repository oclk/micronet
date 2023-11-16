using AutoMapper;
using IdentityService.Application.Common.Interfaces.Clients;
using IdentityService.Application.Common.Models.Clients.Groups;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace IdentityService.Application.Features.Groups.Queries.GetGroupsCount;

/// <summary>
/// Handles the MediatR query to retrieve the group count.
/// </summary>
/// <param name="groupsHttpClient">The HTTP client for making requests to the groups service.</param>
/// <param name="httpContextAccessor">The accessor for retrieving information about the current HTTP request.</param>
/// <param name="mapper">The mapper for handling object mapping and transformation.</param>
public class GetGroupsCountQueryHandler(IGroupsHttpClient groupsHttpClient, IHttpContextAccessor httpContextAccessor, IMapper mapper) : IRequestHandler<GetGroupsCountQuery, GetGroupsCountQueryVm>
{
    /// <summary>
    /// Processes the given query, retrieves the group count, and returns the response.
    /// </summary>
    /// <param name="request">The query to be processed.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The response containing the group count.</returns>
    public async Task<GetGroupsCountQueryVm> Handle(GetGroupsCountQuery request, CancellationToken cancellationToken)
    {
        #region Setup Params
        // Get or create JWT
        string jwtToken = httpContextAccessor.HttpContext.Request.Headers.Authorization.ToString();

        // Generate Headers
        Dictionary<string, string> headers = new()
        {
            { "Authorization", jwtToken }
        };

        // Generate QueryParameters
        Dictionary<string, string> queryParameters = request?.QueryParameters?.GetType()
                  .GetProperties()
                  .ToDictionary(prop => prop.Name, prop => prop.GetValue(request?.QueryParameters)?.ToString());
        #endregion

        // Get & Return Response
        GetGroupsCountRequest getGroupsCountRequest = mapper.Map<GetGroupsCountQuery, GetGroupsCountRequest>(request);
        GetGroupsCountResponse getGroupsCountResponse = await groupsHttpClient.GetGroupsCount(getGroupsCountRequest, headers, queryParameters, cancellationToken);
        GetGroupsCountQueryVm response = mapper.Map<GetGroupsCountQueryVm>(getGroupsCountResponse);

        return response;
    }
}
