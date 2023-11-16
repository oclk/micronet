using AutoMapper;
using IdentityService.Application.Common.Interfaces.Clients;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace IdentityService.Application.Features.Groups.Commands.DeleteGroup;

/// <summary>
/// Handles the command to delete a group and its sub-groups.
/// </summary>
/// <param name="groupsHttpClient"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="mapper"></param>
public class DeleteGroupCommandHandler(IGroupsHttpClient groupsHttpClient, IHttpContextAccessor httpContextAccessor, IMapper mapper) : IRequestHandler<DeleteGroupCommand>
{
    /// <summary>
    /// Handles the command to delete a group and its sub-groups.
    /// </summary>
    /// <param name="request">The delete group command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
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
        DeleteGroupRequest deleteGroupRequest = mapper.Map<DeleteGroupRequest>(request);
        await groupsHttpClient.DeleteGroup(deleteGroupRequest, headers, null, cancellationToken);
    }
}
