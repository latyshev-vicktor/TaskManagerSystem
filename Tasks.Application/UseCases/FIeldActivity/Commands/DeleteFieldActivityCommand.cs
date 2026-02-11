using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Tasks.Application.UseCases.FIeldActivity.Commands
{
    public record DeleteFieldActivityCommand(Guid Id, Guid UserId) : IRequest<IExecutionResult>;
}
