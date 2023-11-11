using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers.V1;

[Route("IdentityService/Api/V{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class GroupsController : BaseController
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
}
