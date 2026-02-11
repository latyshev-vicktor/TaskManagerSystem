namespace Tasks.Domain.Entities
{
    public class SprintFieldActivityEntity
    {
        public Guid Id { get; set; }
        public Guid FieldActivityId { get; set; }
        public FieldActivityEntity? FieldActivity { get; set; }
        public Guid SprintId { get; set; }
        public SprintEntity? Sprint { get; set; }
    }
}
