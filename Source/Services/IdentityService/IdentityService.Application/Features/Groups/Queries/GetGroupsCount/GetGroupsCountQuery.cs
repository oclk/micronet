using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shared.Helpers;

namespace IdentityService.Application.Features.Groups.Queries.GetGroupsCount;

/// <summary>
/// Represents a query to retrieve the count of groups with optional filtering parameters.
/// </summary>
public class GetGroupsCountQuery : IRequest<GetGroupsCountQueryResponse>
{
    /// <summary>
    /// Gets or sets the realm from which to retrieve the group count.
    /// </summary>
    public string Realm { get; set; }

    /// <summary>
    /// Gets or sets parameters to filter the group count.
    /// </summary>
    public GetGroupsCountQueryParameters QueryParameters { get; set; }
}

/// <summary>
/// Represents the response object containing the count of groups.
/// </summary>
public class GetGroupsCountQueryResponse
{
    /// <summary>
    /// Gets or sets the total count of groups.
    /// </summary>
    public long Count { get; set; }
}

/// <summary>
/// Represents query parameters used to filter the group count.
/// </summary>
public class GetGroupsCountQueryParameters
{
    /// <summary>
    /// Gets or sets the property for searching groups.
    /// </summary>
    public string Search { get; set; }

    /// <summary>
    /// Gets or sets a boolean property indicating whether to fetch a specific number of top groups.
    /// </summary>
    public bool Top { get; set; }
}

/// <summary>
/// Handles the MediatR query to retrieve the group count.
/// </summary>
public class GetGroupsCountQueryHandler : IRequestHandler<GetGroupsCountQuery, GetGroupsCountQueryResponse>
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
    public GetGroupsCountQueryHandler(HttpClientHelper httpClientHelper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientHelper = httpClientHelper;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Processes the given query, retrieves the group count, and returns the response.
    /// </summary>
    /// <param name="request">The query to be processed.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The response containing the group count.</returns>
    public async Task<GetGroupsCountQueryResponse> Handle(GetGroupsCountQuery request, CancellationToken cancellationToken)
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
        string url = $"{_configuration["Keycloak:RealmUrl"]}/admin/realms/{request.Realm}/groups/count";
        #endregion

        // Get & Return Response
        GetGroupsCountQueryResponse response = await _httpClientHelper.SendRESTRequestAsync<object, GetGroupsCountQueryResponse>(HttpMethod.Get, url, null, headers: headers, queryParameters: queryParameters);

        return response;
    }
}
