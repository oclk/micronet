using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shared.Helpers;

namespace IdentityService.Application.Features.Groups.Commands.UpdateGroupManagementPermissions;

/// <summary>
/// Handles the command to update management permissions associated with a specific group.
/// </summary>
public class UpdateGroupManagementPermissionsCommandHandler : IRequestHandler<UpdateGroupManagementPermissionsCommand, UpdateGroupManagementPermissionsCommandManagementPermissionReference>
{
    private readonly HttpClientHelper _httpClientHelper;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateGroupManagementPermissionsCommandHandler"/> class.
    /// </summary>
    /// <param name="httpClientHelper">The HTTP client helper.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="httpContextAccessor">The HTTP context accessor.</param>
    public UpdateGroupManagementPermissionsCommandHandler(HttpClientHelper httpClientHelper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientHelper = httpClientHelper;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Handles the command to update management permissions associated with a specific group.
    /// </summary>
    /// <param name="request">The update group management permissions command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The reference to the updated management permissions of the group.</returns>
    public async Task<UpdateGroupManagementPermissionsCommandManagementPermissionReference> Handle(UpdateGroupManagementPermissionsCommand request, CancellationToken cancellationToken)
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
        string url = $"{_configuration["Keycloak:RealmUrl"]}/admin/realms/{request.Realm}/groups/{request.Id}/management/permissions";
        #endregion

        // Get & Return Response
        UpdateGroupManagementPermissionsCommandManagementPermissionReference response = await _httpClientHelper.SendRESTRequestAsync<UpdateGroupManagementPermissionsCommandManagementPermissionReference, UpdateGroupManagementPermissionsCommandManagementPermissionReference>(HttpMethod.Put, url, request.UpdateGroupManagementPermissionsCommandManagementPermissionReference, headers: headers);

        return response;
    }
}
