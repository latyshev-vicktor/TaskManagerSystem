namespace Tasks.Application.Dto
{
    public class TargetDto : BaseDto
    {
        public string Name { get; set; }
        public long SprintFieldActivityId { get; set; }
        public List<TaskDto> Tasks = [];
    }
}
