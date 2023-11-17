using AutoMapper;
using IdentityService.Application.Common.Interfaces.Clients;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace IdentityService.Application.Features.Groups.Commands.SetOrCreateSubGroup;

/// <summary>
/// Handles the command to set or create a sub-group within a specified group.
/// </summary>
/// <param name="groupsHttpClient"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="mapper"></param>
public class SetOrCreateSubGroupCommandHandler(IGroupsHttpClient groupsHttpClient, IHttpContextAccessor httpContextAccessor, IMapper mapper) : IRequestHandler<SetOrCreateSubGroupCommand, SetOrCreateSubGroupCommandGroupRepresentation>
{
    /// <summary>
    /// Handles the command to set or create a sub-group within a specified group.
    /// </summary>
    /// <param name="request">The set or create sub-group command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The representation of the created or updated sub-group.</returns>
    public async Task<SetOrCreateSubGroupCommandGroupRepresentation> Handle(SetOrCreateSubGroupCommand request, CancellationToken cancellationToken)
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
        SetOrCreateSubGroupRequest setOrCreateSubGroupRequest = mapper.Map<SetOrCreateSubGroupRequest>(request);
        GroupRepresentation groupRepresentation = await groupsHttpClient.SetOrCreateSubGroup(setOrCreateSubGroupRequest, headers, null, cancellationToken);
        SetOrCreateSubGroupCommandGroupRepresentation response = mapper.Map<SetOrCreateSubGroupCommandGroupRepresentation>(groupRepresentation);

        return response;
    }
}
