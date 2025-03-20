using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Tasks.Application.UseCases
{
    public record CreateSprintCommand(
        long UserId,
        string Name, 
        string Description, 
        DateTimeOffset StartDate,
        DateTimeOffset EndDate) : IRequest<IExecutionResult<long>>;
}
