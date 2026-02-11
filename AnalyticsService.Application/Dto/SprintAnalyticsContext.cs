namespace AnalyticsService.Application.Dto
{
    public class SprintAnalyticsContext
    {
        public Guid SprintId { get; }
        public string Name { get; }
        public Guid UserId { get; }

        public int TotalTasks { get; }
        public int CompletedTasks { get; }

        public double CompletionRate =>
            TotalTasks == 0 ? 0 : (double)CompletedTasks / TotalTasks;

        public int TasksAddedAfterStart { get; }

        public int BadSprintsInRow { get; }

        public SprintAnalyticsContext()
        {
            
        }

        public SprintAnalyticsContext(
            Guid sprintId,
            Guid userId,
            int totalTasks,
            int completedTasks,
            string name,
            int tasksAddedAfterStart = 0,
            int badSprintsInRow = 0
            )
        {
            SprintId = sprintId;
            UserId = userId;
            TotalTasks = totalTasks;
            CompletedTasks = completedTasks;
            Name = name;
            TasksAddedAfterStart = tasksAddedAfterStart;
            BadSprintsInRow = badSprintsInRow;
        }
    }
}
