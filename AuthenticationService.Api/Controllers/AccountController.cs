using AuthenticationService.Api.Contracts;
using AuthenticationService.Application.UseCases.User.Commands;
using AuthenticationService.Application.UseCases.User.Dto;
using AuthenticationService.Application.UseCases.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskManagerSystem.Common.Options;

namespace AuthenticationService.Api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccountController(IMediator mediator, IOptions<JwtSettings> options) : ControllerBase
    {
        private readonly JwtSettings _jwtSettings = options.Value;

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]CreateUserDto dto)
        {
            var command = new CreateUserRequest(dto);
            var result = await mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error.Message);

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginFormRequest request)
        {
            var query = new LoginQuery(request.Email, request.Password);
            var response = await mediator.Send(query);

            SetResponseCookies(response.Value.AccessToken);

            return NoContent();
        }

        private void SetResponseCookies(string accessToken)
        {
            var cookieOption = new CookieOptions()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddDays(_jwtSettings.DaysToExpirationAccessToken),
                Secure = true
            };

            Response.Cookies.Append("access_token", accessToken, cookieOption);
        }
    }
}
