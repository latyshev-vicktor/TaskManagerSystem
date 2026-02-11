using TaskManagerSystem.Common.Dtos;

namespace Tasks.Application.UseCases.Sprint.Dto
{
    public class SprintFilter : BaseFilter
    {
        public Guid? UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string? Status { get; set; }
        public Guid? FieldActivityId { get; set; }
        public Guid[]? FieldActivityIds { get; set; }
    }
}
