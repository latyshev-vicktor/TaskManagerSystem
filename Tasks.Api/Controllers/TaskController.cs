using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagerSystem.Common.Extensions;
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

        [HttpGet("statuses")]
        public async Task<ActionResult> GetSprintStatus()
        {
            var query = new GetTaskStatusesQuery();
            var result = await mediator.Send(query);

            return Ok(result.Value);
        }
    }
}
