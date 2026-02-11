namespace Tasks.Application.Dto
{
    public class SprintDto : BaseDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public SprintStatusDto SprintStatus { get; set; }
        public List<FieldActivityForSprintDto> FieldActivities { get; set; } = [];
    }
}
