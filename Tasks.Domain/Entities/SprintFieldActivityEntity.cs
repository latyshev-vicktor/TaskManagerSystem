namespace Tasks.Domain.Entities
{
    public class SprintFieldActivityEntity
    {
        public long FieldActivityId { get; set; }
        public FieldActivityEntity? FieldActivity { get; set; }
        public long SprintId { get; set; }
        public SprintEntity? Sprint { get; set; }
    }
}
