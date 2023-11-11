using IdentityService.Application.Features.Groups.Queries.GetGroupsCount;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shared.Helpers;

namespace IdentityService.Application.Features.Groups.Queries.GetGroups;

/// <summary>
/// Represents a query to retrieve groups with optional filtering parameters.
/// </summary>
public class GetGroupsQuery : IRequest<List<GetGroupsQueryResponse>>
{
    /// <summary>
    /// Gets or sets the realm from which to retrieve the groups.
    /// </summary>
    public string Realm { get; set; }

    /// <summary>
    /// Gets or sets parameters to filter the groups.
    /// </summary>
    public GetGroupsQueryParameters QueryParameters { get; set; }
}

/// <summary>
/// Represents the response object containing group details.
/// </summary>
public class GetGroupsQueryResponse
{
    /// <summary>
    /// Gets or sets the identifier of the group.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the group.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the path of the group.
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Gets or sets subgroups of the current group.
    /// </summary>
    public List<GetGroupsQueryResponse> SubGroups { get; set; }
}

/// <summary>
/// Represents query parameters used to filter the group details.
/// </summary>
public class GetGroupsQueryParameters
{
    /// <summary>
    /// Gets or sets a boolean property indicating whether to use brief representation.
    /// </summary>
    public bool BriefRepresentation { get; set; }

    /// <summary>
    /// Gets or sets a boolean property indicating whether to perform an exact match.
    /// </summary>
    public bool Exact { get; set; }

    /// <summary>
    /// Gets or sets the first result to retrieve.
    /// </summary>
    public string First { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of results to retrieve.
    /// </summary>
    public string Max { get; set; }

    /// <summary>
    /// Gets or sets a boolean property indicating whether to populate the hierarchy.
    /// </summary>
    public bool PopulateHierarchy { get; set; }

    /// <summary>
    /// Gets or sets the search query.
    /// </summary>
    public string Q { get; set; }

    /// <summary>
    /// Gets or sets the search string.
    /// </summary>
    public string Search { get; set; }
}

/// <summary>
/// Handles the MediatR query to retrieve group details.
/// </summary>
public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, List<GetGroupsQueryResponse>>
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