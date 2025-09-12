namespace Tasks.Application.Dto
{
    public class SprintTableDto : BaseDto
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public SprintStatusDto SprintStatus { get; set; }
        public List<FieldActivityShortDto> FieldActivities { get; set; } = [];
    }

    public class FieldActivityShortDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
