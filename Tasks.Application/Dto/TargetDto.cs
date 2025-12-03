namespace Tasks.Application.Dto
{
    public class TargetDto : BaseDto
    {
        public string Name { get; set; }
        public long SprintId { get; set; }
        public SprintDto? Sprint { get; set; }
        public List<TaskDto> Tasks = [];
    }
}
