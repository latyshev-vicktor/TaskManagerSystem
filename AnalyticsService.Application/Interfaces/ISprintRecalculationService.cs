namespace AnalyticsService.Application.Interfaces
{
    public interface ISprintRecalculationService
    {
        Task RecalculateSprint(long sprintId, long userId);
    }
}
