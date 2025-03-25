using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthenticationService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(ClaimsPrincipal claimsPrincipal) : ControllerBase
    {
        [HttpGet("me")]
        [Authorize]
        public Dictionary<string, string> Get()
        {
            return claimsPrincipal.Claims.ToDictionary(c => c.Type, y => y.Value);
        }
    }
}
