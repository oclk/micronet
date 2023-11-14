using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shared.Helpers;

namespace IdentityService.Application.Features.Groups.Queries.GetGroups;

/// <summary>
/// Handles the MediatR query to retrieve group details.
/// </summary>
public class GetGroupsQueryHandler
{
    private readonly HttpClientHelper _httpClientHelper;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetGroupsCountQueryHandler"/> class.
    /// </summary>
    /// <param name="httpClientHelper">An auxiliary class for handling HTTP requests.</param>
    /// <param name="configuration">Represents the application configuration.</param>
    /// <param name="httpContextAccessor">An accessor for the HTTP context.</param>
    public GetGroupsQueryHandler(HttpClientHelper httpClientHelper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientHelper = httpClientHelper;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Processes the given query, retrieves the groups, and returns the response.
    /// </summary>
    /// <param name="request">The query to be processed.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The response containing the group details.</returns>
    public async Task<List<GetGroupsQueryResponse>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
    {
        #region Setup Params
        // Get or create JWT.
        string jwtToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        // Generate Headers
        Dictionary<string, string> headers = new()
        {
            { "Authorization", jwtToken }
        };

        // Generate QueryParameters
        Dictionary<string, string> queryParameters = request?.QueryParameters?.GetType()
                  .GetProperties()
                  .ToDictionary(prop => prop.Name, prop => prop.GetValue(request?.QueryParameters)?.ToString());

        // Create Url
        string url = $"{_configuration["Keycloak:RealmUrl"]}/admin/realms/{request.Realm}/groups";
        #endregion

        // Get & Return Response
        List<GetGroupsQueryResponse> response = await _httpClientHelper.SendRESTRequestAsync<object, List<GetGroupsQueryResponse>>(HttpMethod.Get, url, null, headers: headers, queryParameters: queryParameters);

        return response;
    }
}
