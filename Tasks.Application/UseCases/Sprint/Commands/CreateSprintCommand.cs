using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public record CreateSprintCommand(
        long UserId,
        string Name, 
        string Description, 
        DateTimeOffset StartDate,
        DateTimeOffset EndDate) : IRequest<IExecutionResult<long>>;
}
