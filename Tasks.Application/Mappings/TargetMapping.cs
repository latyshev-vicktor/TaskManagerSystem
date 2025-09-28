using Tasks.Application.Dto;
using Tasks.Domain.Entities;

namespace Tasks.Application.Mappings
{
    public static class TargetMapping
    {
        public static TargetDto ToDto(this TargetEntity entity)
        {
            return new TargetDto
            {
                Id = entity.Id,
                CreatedDate = entity.CreatedDate,
                SprintId = entity.SprintId,
                Name = entity.Name.Name,
                Tasks = [.. entity.Tasks.Select(x => x.ToDto())],
            };
        }
    }
}
