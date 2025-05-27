using TaskManagerSystem.Common.Dtos;

namespace Tasks.Application.UseCases.Sprint.Dto
{
    public class SprintFilter : BaseFilter
    {
        public long? UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string? Status { get; set; }
        public long? FieldActivityId { get; set; }
    }
}
