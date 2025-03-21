using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Tasks.Application.UseCases.FIeldActivity.Commands
{
    public record CreateFieldActivityCommand(string Name, long UserId) : IRequest<IExecutionResult<long>>;
}
