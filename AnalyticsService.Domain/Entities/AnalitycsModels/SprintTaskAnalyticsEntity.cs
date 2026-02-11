using AnalyticsService.Domain.Enums;

namespace AnalyticsService.Domain.Entities.AnalitycsModels
{
    public class SprintTaskAnalyticsEntity
    {
        public Guid SprintId { get; set; }
        public Guid TaskId { get; set; }
        public TasksStatus Status { get; set; }

        private SprintTaskAnalyticsEntity()
        {
        }

        public SprintTaskAnalyticsEntity(Guid sprintId, Guid taskId)
        {
            SprintId = sprintId;
            TaskId = taskId;
            Status = TasksStatus.Created;
        }

        public void UpdateStatus(TasksStatus status)
        {
            Status = status;
        }
    }
}
