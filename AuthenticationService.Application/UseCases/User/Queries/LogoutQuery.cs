using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Application.UseCases.User.Queries
{
    public record LogoutQuery(Guid UserId) : IRequest<IExecutionResult>;
}
