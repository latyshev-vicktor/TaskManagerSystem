namespace Tasks.Application.UseCases.Sprint.Dto
{
    public class SprintFilter
    {
        public long? Id { get; set; }
        public long? UserId { get; private set; }
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public DateTimeOffset? StartDate { get; private set; }
        public DateTimeOffset? EndDate { get; private set; }
        public string? Status { get; private set; }
        public long? FieldActivityId { get; private set; }
    }
}
