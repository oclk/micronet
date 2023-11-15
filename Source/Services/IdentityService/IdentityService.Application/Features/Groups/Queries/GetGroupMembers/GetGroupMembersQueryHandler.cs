using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shared.Helpers;

namespace IdentityService.Application.Features.Groups.Queries.GetGroupMembers;

/// <summary>
/// Handles the retrieval of group members based on the provided query parameters.
/// </summary>
public class GetGroupMembersQueryHandler : IRequestHandler<GetGroupMembersQuery, GetGroupMembersQueryResponse>
{
    private readonly HttpClientHelper _httpClientHelper;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the GetGroupMembersQueryHandler class.
    /// </summary>
    public GetGroupMembersQueryHandler(HttpClientHelper httpClientHelper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientHelper = httpClientHelper;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Handles the GetGroupMembersQuery to retrieve group members using the provided parameters.
    /// </summary>
    /// <param name="request">The GetGroupMembersQuery request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A response containing the group members based on the query.</returns>
    public async Task<GetGroupMembersQueryResponse> Handle(GetGroupMembersQuery request, CancellationToken cancellationToken)
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
        string url = $"{_configuration["Keycloak:RealmUrl"]}/admin/realms/{request.Realm}/groups{request.Id}/members";
        #endregion

        // Get & Return Response
        GetGroupMembersQueryResponse response = await _httpClientHelper.SendRESTRequestAsync<object, GetGroupMembersQueryResponse>(HttpMethod.Get, url, null, headers: headers, queryParameters: queryParameters);

        return response;
    }
}
