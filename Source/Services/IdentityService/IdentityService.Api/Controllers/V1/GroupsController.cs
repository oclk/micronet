using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers.V1;

[Route("IdentityService/Api/V{version:apiVersion}/{realm}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class GroupsController : BaseController
{
    [HttpGet("Count")]
    public IActionResult GetGroupsCount(string realm)
    {
        return Ok();
    }

    [HttpGet]
    public IActionResult GetGroups(string realm)
    {
        return Ok();
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
