using Asp.Versioning;
using IdentityService.Application.Features.Groups.Queries.GetGroups;
using IdentityService.Application.Features.Groups.Queries.GetGroupsCount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers.V1;

[Route("IdentityService/Api/V{version:apiVersion}/{realm}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class GroupsController : BaseController
{
    /// <summary>
    /// Returns the groups counts.
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="queryParameters"></param>
    /// <returns></returns>
    [HttpGet("Count")]
    public async Task<GetGroupsCountQueryResponse> GetGroupsCount(string realm, [FromQuery] GetGroupsCountQueryParameters queryParameters)
    {
        GetGroupsCountQuery getGroupsCountQuery = new()
        {
            Realm = realm,
            QueryParameters = queryParameters,
        };
        return await Mediator.Send(getGroupsCountQuery);
    }

    /// <summary>
    /// Get group hierarchy. Only name and ids are returned.
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="queryParameters"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<GetGroupsQueryResponse>> GetGroups(string realm, [FromQuery] GetGroupsQueryParameters queryParameters)
    {
        GetGroupsQuery getGroupsQuery = new()
        {
            Realm = realm,
            QueryParameters = queryParameters,
        };
        return await Mediator.Send(getGroupsQuery);
    }

    [HttpPost("{id}/Children")]
    public IActionResult SetOrCreateGroup(string realm, string id)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteGroup(string realm, string id)
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetGroup(string realm, string id)
    {
        return Ok();
    }

    [HttpGet("{id}/Management/Permissions")]
    public IActionResult GetGroupManagementPermissions(string realm, string id)
    {
        return Ok();
    }

    [HttpPut("{id}/Management/Permissions")]
    public IActionResult UpdateGroupManagementPermissions(string realm, string id)
    {
        return Ok();
    }

    [HttpGet("{id}/Members")]
    public IActionResult GetGroupMembers(string realm, string id)
    {
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateGroup(string realm, string id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult CreateGroup(string realm, string id)
    {
        return Ok();
    }
}
