using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Tasks.Application.UseCases.FIeldActivity.Commands
{
    public record CreateFieldActivityCommand(string Name, Guid UserId) : IRequest<IExecutionResult<Guid>>;
}
