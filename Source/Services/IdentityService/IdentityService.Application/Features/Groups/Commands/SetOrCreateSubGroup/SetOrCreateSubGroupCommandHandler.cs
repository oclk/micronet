using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shared.Helpers;

namespace IdentityService.Application.Features.Groups.Commands.SetOrCreateSubGroup;

/// <summary>
/// Handles the command to set or create a sub-group within a specified group.
/// </summary>
public class SetOrCreateSubGroupCommandHandler : IRequestHandler<SetOrCreateSubGroupCommand, SetOrCreateSubGroupCommandGroupRepresentation>
{
    private readonly HttpClientHelper _httpClientHelper;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetOrCreateSubGroupCommandHandler"/> class.
    /// </summary>
    /// <param name="httpClientHelper">The HTTP client helper.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="httpContextAccessor">The HTTP context accessor.</param>
    public SetOrCreateSubGroupCommandHandler(HttpClientHelper httpClientHelper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientHelper = httpClientHelper;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Handles the command to set or create a sub-group within a specified group.
    /// </summary>
    /// <param name="request">The set or create sub-group command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The representation of the created or updated sub-group.</returns>
    public async Task<SetOrCreateSubGroupCommandGroupRepresentation> Handle(SetOrCreateSubGroupCommand request, CancellationToken cancellationToken)
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
        string url = $"{_configuration["Keycloak:RealmUrl"]}/admin/realms/{request.Realm}/groups/{request.Id}/children";
        #endregion

        // Get & Return Response
        SetOrCreateSubGroupCommandGroupRepresentation response = await _httpClientHelper.SendRESTRequestAsync<SetOrCreateSubGroupCommand, SetOrCreateSubGroupCommandGroupRepresentation>(HttpMethod.Post, url, request, headers: headers);

        return response;
    }
}
