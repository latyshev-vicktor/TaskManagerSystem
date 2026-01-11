using AnalyticsService.Domain.Enums;

namespace AnalyticsService.Domain.Entities.AnalitycsModels
{
    public class SprintTaskAnalyticsEntity
    {
        public long SprintId { get; set; }
        public long TaskId { get; set; }
        public TasksStatus Status { get; set; }

        private SprintTaskAnalyticsEntity()
        {
        }

        public SprintTaskAnalyticsEntity(long sprintId, long taskId)
        {
            SprintId = sprintId;
            TaskId = taskId;
            Status = TasksStatus.Created;
        }
    }
}
