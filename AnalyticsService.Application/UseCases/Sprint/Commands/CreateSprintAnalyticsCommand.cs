using MediatR;

namespace AnalyticsService.Application.UseCases.Sprints.Commands
{
    public record CreateSprintAnalyticsCommand(
        Guid SprintId,
        Guid UserId,
        string Name) : IRequest;
}
