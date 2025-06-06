﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using TaskManagerSystem.Common.Dtos;
using TaskManagerSystem.Common.Extensions;
using Tasks.Application.Dto;
using Tasks.Application.UseCases.Sprint.Commands;
using Tasks.Application.UseCases.Sprint.Dto;
using Tasks.Application.UseCases.Sprint.Queries;

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
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost("allByFilter")]
        public async Task<ActionResult<List<SprintDto>>> GetAllByFilter([FromBody]SprintFilter filter)
        {
            var query = new GetAllSprintByFilterQuery(filter);
            var result = await mediator.Send(query);

            return Ok(result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SprintDto dto)
        {
            var command = new UpdateSprintCommand(dto);
            var result = await mediator.Send(command);

            return Ok(result.Value);
        }

        [HttpPost("search")]
        public async Task<ActionResult<SearchData<SprintDto>>> Search([FromBody]SprintFilter filter)
        {
            var query = new SearchSprintQuery(filter);
            var result = await mediator.Send(query);

            return Ok(result.Value);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute][Required] long id)
        {
            var query = new GetSprintByIdQuery(id);
            var sprintResult = await mediator.Send(query);

            return Ok(sprintResult.Value);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var command = new DeleteSprintCommand(id);
            var result = await mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}
