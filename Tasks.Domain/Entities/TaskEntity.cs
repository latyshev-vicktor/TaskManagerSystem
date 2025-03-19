namespace Tasks.Domain.Entities
{
    public class TaskEntity : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string StatusId { get; private set; }
        public long TargetId { get; private set; }
        public TargetEntity? Target { get; private set; }
        public TaskStatusEntity Status { get; private set; }
    }
}
