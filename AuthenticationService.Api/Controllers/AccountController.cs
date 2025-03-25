using AuthenticationService.Application.UseCases.User.Commands;
using AuthenticationService.Application.UseCases.User.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]CreateUserDto dto)
        {
            var command = new CreateUserRequest(dto);
            var result = await mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error.Message);

            return NoContent();
        }
    }
}
