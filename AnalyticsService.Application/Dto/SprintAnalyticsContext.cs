namespace AnalyticsService.Application.Dto
{
    public class SprintAnalyticsContext
    {
        public long SprintId { get; }
        public long UserId { get; }

        public int TotalTasks { get; }
        public int CompletedTasks { get; }

        public double CompletionRate =>
            TotalTasks == 0 ? 0 : (double)CompletedTasks / TotalTasks;

        public int TasksAddedAfterStart { get; }

        public int BadSprintsInRow { get; }
    }
}
