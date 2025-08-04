using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tasks.Application.UseCases.Task.Commands;
using Tasks.Application.UseCases.Task.Dto;

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

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
    }
}
