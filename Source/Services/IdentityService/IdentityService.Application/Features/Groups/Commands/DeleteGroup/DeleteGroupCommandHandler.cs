using IdentityService.Application.Features.Groups.Commands.SetOrCreateSubGroup;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shared.Helpers;

namespace IdentityService.Application.Features.Groups.Commands.DeleteGroup;

/// <summary>
/// Handles the command to delete a group and its sub-groups.
/// </summary>
public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand>
{
    private readonly HttpClientHelper _httpClientHelper;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteGroupCommandHandler"/> class.
    /// </summary>
    /// <param name="httpClientHelper">The HTTP client helper.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="httpContextAccessor">The HTTP context accessor.</param>
    public DeleteGroupCommandHandler(HttpClientHelper httpClientHelper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientHelper = httpClientHelper;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Handles the command to delete a group and its sub-groups.
    /// </summary>
    /// <param name="request">The delete group command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        #region Setup Params
        // Get or create JWT.
        string jwtToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        // Generate Headers
        Dictionary<string, string> headers = new()
        {
            { "Authorization", jwtToken }
        };

        // Create Url
        string url = $"{_configuration["Keycloak:RealmUrl"]}/admin/realms/{request.Realm}/groups/{request.Id}";
        #endregion

        // Get & Return Response
        var response = await _httpClientHelper.SendRESTRequestAsync<SetOrCreateSubGroupCommand, SetOrCreateSubGroupCommandGroupRepresentation>(HttpMethod.Delete, url, headers: headers);
    }
}
