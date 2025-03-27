using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using TaskManagerSystem.Common.Extensions;
using Tasks.Application.Dto;
using Tasks.Application.UseCases.FIeldActivity.Commands;
using Tasks.Application.UseCases.FIeldActivity.Dto;
using Tasks.Application.UseCases.FIeldActivity.Queires;

namespace Tasks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldActivityController(IMediator mediator, IPrincipal principal) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateFieldActivityDto request)
        {
            var command = new CreateFieldActivityCommand(request.Name, principal.GetUserId());
            var result = await mediator.Send(command);

            if (result.IsFailure)
                return BadRequest();

            return Ok(result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] FieldActivityDto request)
        {
            var userId = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            request.UserId = userId;

            var command = new UpdateFieldActivityCommand(request);
            var result = await mediator.Send(command);

            if (result.IsFailure)
                return BadRequest();

            return Ok(result.Value);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var userId = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var command = new DeleteFieldActivityCommand(id, userId);
            await mediator.Send(command);

            return NoContent();
        }

        [HttpGet("my")]
        public async Task<ActionResult<List<FieldActivityDto>>> GetMyFieldActivities()
        {
            var userId = principal.GetUserId();

            var query = new GetMyFieldActivitiesQuery(userId);
            var result = await mediator.Send(query);

            return Ok(result.Value);
        }

        [HttpPost("all")]
        public async Task<ActionResult<List<FieldActivityDto>>> GetAllByFilter([FromBody] FieldActivityFilter filter)
        {
            var query = new GetAllFiledActivityByFilterQuery(filter);
            var result = await mediator.Send(query);

            return Ok(result.Value);
        }
    }
}
