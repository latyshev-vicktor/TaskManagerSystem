using MediatR;

namespace AnalyticsService.Application.UseCases.Sprints.Commands
{
    public record CreateSprintForAnalitycsCommand(
        Guid SprintId,
        Guid UserId,
        string Name) : IRequest;
}
