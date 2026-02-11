using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using TaskManagerSystem.Common.Dtos;
using TaskManagerSystem.Common.Extensions;
using Tasks.Application.Dto;
using Tasks.Application.UseCases.FIeldActivity.Queires;
using Tasks.Application.UseCases.Sprint.Commands;
using Tasks.Application.UseCases.Sprint.Dto;
using Tasks.Application.UseCases.Sprint.Queries;
using Tasks.Application.UseCases.SprintWeek.Queries;

namespace Tasks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintController(IMediator mediator, IPrincipal principal) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateSprintDto request, CancellationToken cancellationToken)
        {
            var userId = principal.GetUserId();
            var command = new CreateSprintCommand(request, userId);
            var result = await mediator.Send(command, cancellationToken);

            return result.Match(
                () => Ok(result.Value), 
                error => BadRequest(result.Error));
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SprintDto dto, CancellationToken cancellationToken)
        {
            var command = new UpdateSprintCommand(dto);
            var result = await mediator.Send(command, cancellationToken);

            return Ok(result.Value);
        }


        [HttpPost("search")]
        public async Task<ActionResult<SearchData<SprintDto>>> Search([FromBody]SprintFilter filter, CancellationToken cancellationToken)
        {
            var query = new SearchSprintQuery(filter);
            var result = await mediator.Send(query, cancellationToken);

            return Ok(result.Value);
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute][Required] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetSprintByIdQuery(id);
            var sprintResult = await mediator.Send(query, cancellationToken);

            return Ok(sprintResult.Value);
        }


        [HttpGet("{id:guid}/targets")]
        public async Task<IActionResult> GetTargetsBySprintId([FromRoute]Guid id, CancellationToken cancellationToken)
        {
            var query = new GetTargetsBySprintIdQuery(id);
            var targetResult = await mediator.Send(query, cancellationToken);

            return targetResult.Match(() => Ok(targetResult.Value), error => BadRequest(targetResult.Error));
        }

        [HttpGet("{id:guid}/weeks")]
        public async Task<IActionResult> GetWeeksBySprintId([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetWeeksBySprintIdQuery(id);
            var sprintWeekResult = await mediator.Send(query, cancellationToken);

            return sprintWeekResult.Match(() => Ok(sprintWeekResult.Value), error => BadRequest(sprintWeekResult.Error));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteSprintCommand(id);
            var result = await mediator.Send(command, cancellationToken);

            return result.Match(() => NoContent(), error => BadRequest(error));
        }


        [HttpGet("{sprintId:guid}/field-activities")]
        public async Task<ActionResult<List<FieldActivityForSprintDto>>> GetFieldActivitiesBySprint([FromRoute] Guid sprintId, CancellationToken cancellationToken)
        {
            var userId = principal.GetUserId();
            var query = new GetFieldActivitiesBySprintQuery(userId, sprintId);

            var result = await mediator.Send(query, cancellationToken);

            return result.Match(
                () => Ok(result.Value),
                error => BadRequest(error));
        }


        [HttpPatch("{id:guid}/start-sprint")]
        public async Task<ActionResult> StartSprint([FromRoute]Guid id, CancellationToken cancellationToken)
        {
            var userId = principal.GetUserId();
            var command = new StartSprintCommand(userId, id);
            var result = await mediator.Send(command, cancellationToken);

            return result.Match(
                () => NoContent(),
                error => BadRequest(error));
        }


        [HttpGet("statuses")]
        public async Task<ActionResult> GetSprintStatus()
        {
            var query = new GetSprintStatusesQuery();
            var result = await mediator.Send(query);

            return Ok(result.Value);
        }
    }
}
