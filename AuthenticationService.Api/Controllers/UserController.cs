using AuthenticationService.Application.UseCases.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using TaskManagerSystem.Common.Extensions;

namespace AuthenticationService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator, IPrincipal principal) : ControllerBase
    {
        [HttpGet("short-information")]
        public async Task<IActionResult> GetShortInformation()
        {
            var userId = principal.GetUserId();
            var query = new GetShortInformationQuery(userId);
            var result = await mediator.Send(query);

            return Ok(result.Value);
        }
    }
}
