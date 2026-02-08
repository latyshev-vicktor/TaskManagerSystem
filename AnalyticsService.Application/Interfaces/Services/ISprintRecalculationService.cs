namespace AnalyticsService.Application.Interfaces.Services
{
    public interface ISprintRecalculationService
    {
        Task RecalculateSprint(long sprintId, long userId);
    }
}
