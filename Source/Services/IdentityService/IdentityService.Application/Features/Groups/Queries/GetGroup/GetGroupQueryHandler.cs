using AutoMapper;
using IdentityService.Application.Common.Interfaces.Clients;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace IdentityService.Application.Features.Groups.Queries.GetGroup;

/// <summary>
/// Handles the query to retrieve information about a specific group.
/// </summary>
/// <param name="groupsHttpClient"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="mapper"></param>
public class GetGroupQueryHandler(IGroupsHttpClient groupsHttpClient, IHttpContextAccessor httpContextAccessor, IMapper mapper) : IRequestHandler<GetGroupQuery, GetGroupQueryVm>
{
    /// <summary>
    /// Handles the query to retrieve information about a specific group.
    /// </summary>
    /// <param name="request">The get group query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The representation of the retrieved group.</returns>
    public async Task<GetGroupQueryVm> Handle(GetGroupQuery request, CancellationToken cancellationToken)
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
        GetGroupRequest getGroupRequest = mapper.Map<GetGroupRequest>(request);
        GroupRepresentation groupRepresentation = await groupsHttpClient.GetGroup(getGroupRequest, headers, null, cancellationToken);
        GetGroupQueryVm response = mapper.Map<GetGroupQueryVm>(groupRepresentation);

        return response;
    }
}
