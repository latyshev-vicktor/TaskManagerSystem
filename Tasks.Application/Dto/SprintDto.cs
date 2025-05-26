namespace Tasks.Application.Dto
{
    public class SprintDto : BaseDto
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public SprintStatusDto SprintStatus { get; set; }
        public long FieldActivityId { get; set; }
        public FieldActivityDto? FieldActivity { get; set; }
        public List<TargetDto> Targets { get; set; }
    }
}
