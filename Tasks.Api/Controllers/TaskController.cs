using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagerSystem.Common.Extensions;
using Tasks.Application.Dto;
using Tasks.Application.UseCases.Task.Commands;
using Tasks.Application.UseCases.Task.Dto;
using Tasks.Application.UseCases.Task.Queries;

namespace Tasks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateTask([FromBody]CreateTaskDto dto)
        {
            var command = new CreateTaskCommand(dto);
            var result = await mediator.Send(command);

            return result.Match(
                () => Ok(result.Value),
                error => BadRequest(error));
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]TaskDto dto)
        {
            var command = new UpdateTaskCommand(dto);
            var result = await mediator.Send(command);

            return result.Match(
                () => Ok(result.Value),
                error => BadRequest(error));
        }

        [HttpGet("statuses")]
        public async Task<ActionResult> GetSprintStatus()
        {
            var query = new GetTaskStatusesQuery();
            var result = await mediator.Send(query);

            return Ok(result.Value);
        }

        [HttpPatch("{id:long}/complete")]
        public async Task<ActionResult> CompleteTask([FromRoute]long id)
        {
            var command = new CompleteTaskCommand(id);
            var result = await mediator.Send(command);

            return result.Match(
                () => Ok(),
                error => BadRequest(error));
        }

        [HttpPatch("{id:long}/set-created")]
        public async Task<ActionResult> SetCreated([FromRoute]long id)
        {
            var command = new SetCreatedTaskCommand(id);
            var result = await mediator.Send(command);

            return result.Match(
                () => Ok(),
                error => BadRequest(error));
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult> Delete([FromRoute]long id)
        {
            var command = new DeleteTaskCommand(id);
            await mediator.Send(command);

            return NoContent();
        }
    }
}
