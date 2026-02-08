namespace AnalyticsService.Domain.Entities.AnalitycsModels
{
    public class SprintAnalyticsEntity
    {
        public Guid Id { get; private set; }
        public long UserId { get; private set; }
        public long SprintId { get; set; }
        public DateTimeOffset LastUpdatedAt { get; private set; }

        public int TotalTasks { get; private set; }
        public int CompletedTasks { get; private set; }
        public string Name { get; private set; }

        public double CompletionRate =>
            TotalTasks == 0 ? 0 : (double)CompletedTasks / TotalTasks;

        private SprintAnalyticsEntity()
        {
            
        }

        public SprintAnalyticsEntity(long userId, long sprintId, string name)
        {
            UserId = userId;
            SprintId = sprintId;
            LastUpdatedAt = DateTimeOffset.UtcNow;
            Name = name;
        }

        public void Update(int totalTasks, int completedTasks)
        {
            TotalTasks = totalTasks;
            CompletedTasks = completedTasks;
            LastUpdatedAt = DateTimeOffset.UtcNow;
        }

        public void UpdateName(string name)
        {
            Name = name;
            LastUpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
