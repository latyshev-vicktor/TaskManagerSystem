using AuthenticationService.Application.UseCases.User.Dto;
using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Application.UseCases.User.Queries
{
    public record RefreshTokenQuery(string RefreshToken) : IRequest<IExecutionResult<TokenResponse>>;
}
