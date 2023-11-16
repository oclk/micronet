using AutoMapper;
using IdentityService.Application.Common.Interfaces.Clients;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace IdentityService.Application.Features.Groups.Queries.GetGroupMembers;

/// <summary>
/// Handles the retrieval of group members based on the provided query parameters.
/// </summary>
/// <param name="groupsHttpClient"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="mapper"></param>
public class GetGroupMembersQueryHandler(IGroupsHttpClient groupsHttpClient, IHttpContextAccessor httpContextAccessor, IMapper mapper) : IRequestHandler<GetGroupMembersQuery, GetGroupMembersQueryVm>
{
    /// <summary>
    /// Handles the GetGroupMembersQuery to retrieve group members using the provided parameters.
    /// </summary>
    /// <param name="request">The GetGroupMembersQuery request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A response containing the group members based on the query.</returns>
    public async Task<GetGroupMembersQueryVm> Handle(GetGroupMembersQuery request, CancellationToken cancellationToken)
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
        GetGroupMembersRequest getGroupMembersRequest = mapper.Map<GetGroupMembersRequest>(request);
        GetGroupMembersResponse getGroupMembersResponse = await groupsHttpClient.GetGroupMembers(getGroupMembersRequest, headers, request.QueryParameters, cancellationToken);
        GetGroupMembersQueryVm response = mapper.Map<GetGroupMembersQueryVm>(getGroupMembersResponse);

        return response;
    }
}
