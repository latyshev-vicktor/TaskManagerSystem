using MediatR;

namespace AnalyticsService.Application.UseCases.Tasks.Commands
{
    public record DeleteAnalyticsTaskCommand(Guid TaskId) : IRequest;
}
