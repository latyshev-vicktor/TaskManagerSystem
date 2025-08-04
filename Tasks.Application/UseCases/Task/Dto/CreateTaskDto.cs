namespace Tasks.Application.UseCases.Task.Dto
{
    public class CreateTaskDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long? TargetId { get; set; }
    }
}
