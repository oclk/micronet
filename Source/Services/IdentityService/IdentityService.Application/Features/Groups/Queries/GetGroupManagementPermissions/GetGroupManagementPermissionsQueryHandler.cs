using AutoMapper;
using IdentityService.Application.Common.Interfaces.Clients;
using IdentityService.Application.Common.Models.Clients.Groups;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace IdentityService.Application.Features.Groups.Queries.GetGroupManagementPermissions;

/// <summary>
/// Handles the query to retrieve management permissions associated with a specific group.
/// </summary>
/// <param name="groupsHttpClient"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="mapper"></param>
public class GetGroupManagementPermissionsQueryHandler(IGroupsHttpClient groupsHttpClient, IHttpContextAccessor httpContextAccessor, IMapper mapper) : IRequestHandler<GetGroupManagementPermissionsQuery, GetGroupManagementPermissionsQueryVm>
{
    /// <summary>
    /// Handles the query to retrieve management permissions associated with a specific group.
    /// </summary>
    /// <param name="request">The get group management permissions query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The reference to the management permissions of the group.</returns>
    public async Task<GetGroupManagementPermissionsQueryVm> Handle(GetGroupManagementPermissionsQuery request, CancellationToken cancellationToken)
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
        GetGroupManagementPermissionsRequest getGroupManagementPermissionsRequest = mapper.Map<GetGroupManagementPermissionsRequest>(request);
        GetGroupManagementPermissionsResponse getGroupManagementPermissionsResponse = await groupsHttpClient.GetGroupManagementPermissions(getGroupManagementPermissionsRequest, headers, null, cancellationToken);
        GetGroupManagementPermissionsQueryVm response = mapper.Map<GetGroupManagementPermissionsQueryVm>(getGroupManagementPermissionsResponse);

        return response;
    }
}
