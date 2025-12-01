using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Tasks.Application.UseCases.Task.Commands
{
    public record SetCreatedTaskCommand(long Id) : IRequest<IExecutionResult>;
}
