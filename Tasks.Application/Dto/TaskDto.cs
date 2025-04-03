namespace Tasks.Application.Dto
{
    public class TaskDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStatusDto Status { get; set; }

        public long TargetId { get; set; }
        public TargetDto? Target { get; set; }
    }
}
