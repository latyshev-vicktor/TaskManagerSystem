namespace Tasks.Application.Dto
{
    public class SprintWeekDto : BaseDto
    {
        public long SprintId { get; set; }
        public int WeekNumber { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public List<TaskDto> Tasks { get; set; } = [];
    }
}
