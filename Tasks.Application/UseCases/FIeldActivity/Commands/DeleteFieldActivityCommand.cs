using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Tasks.Application.UseCases.FIeldActivity.Commands
{
    public record DeleteFieldActivityCommand(long Id, long UserId) : IRequest<IExecutionResult>;
}
