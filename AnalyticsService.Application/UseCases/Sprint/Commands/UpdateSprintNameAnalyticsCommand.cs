using MediatR;

namespace AnalyticsService.Application.UseCases.Sprint.Commands
{
    public record UpdateSprintNameAnalyticsCommand(Guid SprintId, string NewName) : IRequest;
}
