using AutoMapper;
using IdentityService.Application.Common.Interfaces.Clients;
using IdentityService.Application.Features.Groups.Queries.GetGroupsCount;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace IdentityService.Application.Features.Groups.Commands.CreateGroup;

/// <summary>
///  Handles the update of a group using the specified Groups HTTP client, HTTP context accessor, and AutoMapper.
/// </summary>
/// <param name="groupsHttpClient"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="mapper"></param>
public class CreateGroupCommandHandler(IGroupsHttpClient groupsHttpClient, IHttpContextAccessor httpContextAccessor, IMapper mapper) : IRequestHandler<CreateGroupCommand, CreateGroupCommandDto>
{
    /// <summary>
    /// Handles the command to create a group.
    /// </summary>
    /// <param name="request">The delete group command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task<CreateGroupCommandDto> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
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
        CreateGroupRequest createGroupRequest = mapper.Map<CreateGroupRequest>(request);
        GroupRepresentation groupRepresentation = await groupsHttpClient.CreateGroup(createGroupRequest, headers, null, cancellationToken);
        CreateGroupCommandDto response = mapper.Map<CreateGroupCommandDto>(groupRepresentation);

        return response;
    }
}
