using AutoMapper;
using IdentityService.Application.Common.Interfaces.Clients;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace IdentityService.Application.Features.Groups.Commands.UpdateGroupManagementPermissions;

/// <summary>
/// Handles the command to update management permissions associated with a specific group.
/// </summary>
/// <param name="groupsHttpClient"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="mapper"></param>
public class UpdateGroupManagementPermissionsCommandHandler(IGroupsHttpClient groupsHttpClient, IHttpContextAccessor httpContextAccessor, IMapper mapper) : IRequestHandler<UpdateGroupManagementPermissionsCommand, UpdateGroupManagementPermissionsCommandDto>
{
    /// <summary>
    /// Handles the command to update management permissions associated with a specific group.
    /// </summary>
    /// <param name="request">The update group management permissions command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The reference to the updated management permissions of the group.</returns>
    public async Task<UpdateGroupManagementPermissionsCommandDto> Handle(UpdateGroupManagementPermissionsCommand request, CancellationToken cancellationToken)
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
        UpdateGroupManagementPermissionsRequest updateGroupManagementPermissionsRequest = mapper.Map<UpdateGroupManagementPermissionsRequest>(request);
        GroupManagementPermissions groupManagementPermissions = await groupsHttpClient.UpdateGroupManagementPermissions(updateGroupManagementPermissionsRequest, headers, null, cancellationToken);
        UpdateGroupManagementPermissionsCommandDto response = mapper.Map<UpdateGroupManagementPermissionsCommandDto>(groupManagementPermissions);

        return response;
    }
}
