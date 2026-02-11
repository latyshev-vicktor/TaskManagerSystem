namespace AnalyticsService.Application.Interfaces.Services
{
    public interface ISprintRecalculationService
    {
        Task RecalculateSprint(Guid sprintId, Guid userId);
    }
}
