using Asp.Versioning;
using IdentityService.Application.Features.Groups.Commands.DeleteGroup;
using IdentityService.Application.Features.Groups.Commands.SetOrCreateSubGroup;
using IdentityService.Application.Features.Groups.Queries.GetGroup;
using IdentityService.Application.Features.Groups.Queries.GetGroupManagementPermissions;
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

    /// <summary>
    /// Set or create child.
    /// This will just set the parent if it exists. Create it and set the parent if the group doesn’t exist.
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="id"></param>
    /// <param name="setOrCreateSubGroupCommandGroupRepresentation"></param>
    /// <returns></returns>
    [HttpPost("{id}/Children")]
    public async Task<SetOrCreateSubGroupCommandGroupRepresentation> SetOrCreateSubGroup(string realm, string id, [FromBody] SetOrCreateSubGroupCommandGroupRepresentation setOrCreateSubGroupCommandGroupRepresentation)
    {
        SetOrCreateSubGroupCommand setOrCreateSubGroupCommand = new()
        {
            Realm = realm,
            Id = id,
            SetOrCreateSubGroupCommandGroupRepresentation = setOrCreateSubGroupCommandGroupRepresentation,
        };
        return await Mediator.Send(setOrCreateSubGroupCommand);
    }

    /// <summary>
    /// This will delete Group.
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task DeleteGroup(string realm, string id)
    {
        DeleteGroupCommand deleteGroupCommand = new()
        {
            Realm = realm,
            Id = id,
        };
        await Mediator.Send(deleteGroupCommand);
    }

    /// <summary>
    /// Get group
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<GetGroupQueryGroupRepresentation> GetGroup(string realm, string id)
    {
        GetGroupQuery getGroupQuery = new()
        {
            Realm = realm,
            Id = id,
        };
        return await Mediator.Send(getGroupQuery);
    }

    /// <summary>
    /// Return object stating whether client Authorization permissions have been initialized or not and a reference
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/Management/Permissions")]
    public async Task<GetGroupManagementPermissionsQueryManagementPermissionReference> GetGroupManagementPermissions(string realm, string id)
    {
        GetGroupManagementPermissionsQuery getGroupManagementPermissionsQuery = new()
        {
            Realm = realm,
            Id = id,
        };
        return await Mediator.Send(getGroupManagementPermissionsQuery);
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
