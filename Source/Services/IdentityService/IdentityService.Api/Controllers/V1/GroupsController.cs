using Asp.Versioning;
using IdentityService.Application.Features.Groups.Commands.CreateGroup;
using IdentityService.Application.Features.Groups.Commands.DeleteGroup;
using IdentityService.Application.Features.Groups.Commands.SetOrCreateSubGroup;
using IdentityService.Application.Features.Groups.Commands.UpdateGroup;
using IdentityService.Application.Features.Groups.Commands.UpdateGroupManagementPermissions;
using IdentityService.Application.Features.Groups.Queries.GetGroup;
using IdentityService.Application.Features.Groups.Queries.GetGroupManagementPermissions;
using IdentityService.Application.Features.Groups.Queries.GetGroupMembers;
using IdentityService.Application.Features.Groups.Queries.GetGroups;
using IdentityService.Application.Features.Groups.Queries.GetGroupsCount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers.V1;

/// <summary>
/// Groups
/// </summary>
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
    public async Task<GetGroupsCountQueryVm> GetGroupsCount(string realm, [FromQuery] Dictionary<string, string> queryParameters)
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
    public async Task<List<GetGroupsQueryVm>> GetGroups(string realm, [FromQuery] Dictionary<string, string> queryParameters)
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
    public async Task<SetOrCreateSubGroupCommandGroupRepresentation> SetOrCreateSubGroup(string realm, string id, [FromBody] SetOrCreateSubGroupCommandGroupRepresentation groupRepresentation)
    {
        SetOrCreateSubGroupCommand setOrCreateSubGroupCommand = new()
        {
            Realm = realm,
            Id = id,
            GroupRepresentation = groupRepresentation,
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
    public async Task<GetGroupQueryVm> GetGroup(string realm, string id)
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
    public async Task<GetGroupManagementPermissionsQueryVm> GetGroupManagementPermissions(string realm, string id)
    {
        GetGroupManagementPermissionsQuery getGroupManagementPermissionsQuery = new()
        {
            Realm = realm,
            Id = id,
        };
        return await Mediator.Send(getGroupManagementPermissionsQuery);
    }

    /// <summary>
    /// Return object stating whether client Authorization permissions have been initialized or not and a reference
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("{id}/Management/Permissions")]
    public async Task<UpdateGroupManagementPermissionsCommandDto> UpdateGroupManagementPermissions(string realm, string id, [FromBody] UpdateGroupManagementPermissionsCommandDto groupManagementPermissions)
    {
        UpdateGroupManagementPermissionsCommand updateGroupManagementPermissionsCommand = new()
        {
            Realm = realm,
            Id = id,
            GroupManagementPermissions = groupManagementPermissions,
        };
        return await Mediator.Send(updateGroupManagementPermissionsCommand);
    }

    /// <summary>
    /// Get users Returns a stream of users, filtered according to query parameters.
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/Members")]
    public async Task<GetGroupMembersQueryVm> GetGroupMembers(string realm, string id, [FromQuery] Dictionary<string, string> queryParameters)
    {
        GetGroupMembersQuery getGroupMembersQuery = new()
        {
            Realm = realm,
            Id = id,
            QueryParameters = queryParameters,
        };
        return await Mediator.Send(getGroupMembersQuery);
    }

    /// <summary>
    /// Update group, ignores subgroups.
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="id"></param>
    /// <param name="groupRepresentation"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<UpdateGroupCommandDto> UpdateGroup(string realm, string id, [FromBody] UpdateGroupCommandDto groupRepresentation)
    {
        UpdateGroupCommand updateGroupCommand = new()
        {
            Realm = realm,
            Id = id,
            GroupRepresentation = groupRepresentation,
        };
        return await Mediator.Send(updateGroupCommand);
    }

    /// <summary>
    /// This will update the group and set the parent if it exists. Create it and set the parent if the group doesn’t exist.
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="id"></param>
    /// <param name="groupRepresentation"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<CreateGroupCommandDto> CreateGroup(string realm, [FromBody] CreateGroupCommandDto groupRepresentation)
    {
        CreateGroupCommand createGroupCommand = new()
        {
            Realm = realm,
            GroupRepresentation = groupRepresentation,
        };
        return await Mediator.Send(createGroupCommand);
    }
}
