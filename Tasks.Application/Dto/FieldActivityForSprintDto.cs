namespace Tasks.Application.Dto
{
    public class FieldActivityForSprintDto : FieldActivityDto
    {
        public Guid SprintId { get; set; }
        public SprintDto? Sprint { get; set; }
    }
}
