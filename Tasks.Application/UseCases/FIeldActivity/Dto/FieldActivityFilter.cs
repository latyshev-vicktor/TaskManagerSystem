using TaskManagerSystem.Common.Dtos;

namespace Tasks.Application.UseCases.FIeldActivity.Dto
{
    public class FieldActivityFilter : BaseFilter
    {
        public string? Name { get; set; }
        public long? UserId { get; set; }
    }
}
