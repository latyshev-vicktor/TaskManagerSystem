namespace Tasks.Domain.Entities
{
    public class SprintEntity : BaseEntity
    {
        public long UserId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
        public string StatusId { get; private set; }
        public SprintStatusEntity? Status { get; private set; }
        private List<TargetEntity> _targets = [];
        public IReadOnlyList<TargetEntity> Targets => _targets;
    }
}
