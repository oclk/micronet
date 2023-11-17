using AutoMapper;
using IdentityService.Application.Common.Interfaces.Clients;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace IdentityService.Application.Features.Groups.Commands.UpdateGroup;

/// <summary>
/// Handles the update of a group using the specified Groups HTTP client, HTTP context accessor, and AutoMapper.
/// </summary>
/// <param name="groupsHttpClient">The HTTP client for interacting with group-related operations.</param>
/// <param name="httpContextAccessor">Accessor for retrieving information about the current HTTP context.</param>
/// <param name="mapper">Automapper for mapping between different types.</param>
public class UpdateGroupCommandHandler(IGroupsHttpClient groupsHttpClient, IHttpContextAccessor httpContextAccessor, IMapper mapper) : IRequestHandler<UpdateGroupCommand, UpdateGroupCommandGroupRepresentation>
{
    /// <summary>
    /// Handles the update group command and returns the updated group representation.
    /// </summary>
    /// <param name="request">The update group command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated group representation.</returns>
    public async Task<UpdateGroupCommandGroupRepresentation> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
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
        UpdateGroupRequest updateGroupRequest = mapper.Map<UpdateGroupRequest>(request);
        GroupRepresentation groupRepresentation = await groupsHttpClient.UpdateGroup(updateGroupRequest, headers, null, cancellationToken);
        UpdateGroupCommandGroupRepresentation response = mapper.Map<UpdateGroupCommandGroupRepresentation>(groupRepresentation);

        return response;
    }
}
