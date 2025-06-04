using AuthenticationService.Application.UseCases.User.Dto;
using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Application.UseCases.User.Queries
{
    public record GetShortInformationQuery(long UserId) : IRequest<IExecutionResult<UserShortDto>>;
}
