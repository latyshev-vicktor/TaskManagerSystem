using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tasks.Application.Dto;
using Tasks.Application.UseCases.Target.Commands;
using Tasks.Application.UseCases.Target.Dto;

namespace Tasks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateTargetDto request)
        {
            var command = new CreateTargetCommand(request);
            var result = await mediator.Send(command);

            return Ok(result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]TargetDto dto)
        {
            var command = new UpdateTargetCommand(dto);
            var result = await mediator.Send(command);

            return Ok(result.Value);
        }
    }
}
