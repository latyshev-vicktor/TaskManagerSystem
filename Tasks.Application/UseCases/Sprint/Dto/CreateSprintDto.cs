namespace Tasks.Application.UseCases.Sprint.Dto
{
    public class CreateSprintDto
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
