using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using TaskManagerSystem.Common.Extensions;
using Tasks.Application.UseCases.Sprint.Commands;
using Tasks.Application.UseCases.Sprint.Dto;

namespace Tasks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintController(IMediator mediator, IPrincipal principal) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateSprintDto request)
        {
            var userId = principal.GetUserId();
            var command = new CreateSprintCommand(request, userId);
            var result = await mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error.Message);

            return Ok(result.Value);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var command = new DeleteSprintCommand(id);
            var result = await mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error.Message);

            return NoContent();
        }
    }
}
