using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Tasks.Application.UseCases.Task.Commands
{
    public record SetCreatedTaskCommand(Guid Id) : IRequest<IExecutionResult>;
}
