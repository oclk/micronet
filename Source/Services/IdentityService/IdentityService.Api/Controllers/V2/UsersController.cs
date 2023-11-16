using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers.V2;

[Route("IdentityService/Api/V{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
[Authorize]
public class UsersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
