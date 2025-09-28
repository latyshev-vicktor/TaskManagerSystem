namespace Tasks.Application.Dto
{
    public class FieldActivityForSprintDto : FieldActivityDto
    {
        public long SprintId { get; set; }
        public SprintDto? Sprint { get; set; }
    }
}
