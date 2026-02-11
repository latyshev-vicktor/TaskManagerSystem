namespace Tasks.Application.Dto
{
    public class SprintTableDto : BaseDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public SprintStatusDto SprintStatus { get; set; }
        public List<FieldActivityShortDto> FieldActivities { get; set; } = [];
    }

    public class FieldActivityShortDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
