using TaskManagerSystem.Common.Dtos;

namespace Tasks.Application.UseCases.FIeldActivity.Dto
{
    public class FieldActivityFilter : BaseFilter
    {
        public string? Name { get; set; }
        public Guid? UserId { get; set; }
    }
}
