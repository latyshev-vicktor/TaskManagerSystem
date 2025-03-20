namespace Tasks.Domain.Entities
{
    public class TargetEntity : BaseEntity
    {
        public string Name { get; private set; }
        public long SprintId { get; private set; }
        public SprintEntity? Sprint { get; private set; }
        private List<TaskEntity> _tasks = [];
        public IReadOnlyList<TaskEntity> Tasks => _tasks;
    }
}
