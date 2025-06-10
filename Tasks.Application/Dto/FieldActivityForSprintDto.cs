namespace Tasks.Application.Dto
{
    public class FieldActivityForSprintDto : FieldActivityDto
    {
        public List<TargetDto> Targets { get; set; } = [];
    }
}
