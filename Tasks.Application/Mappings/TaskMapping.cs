using Tasks.Application.Dto;
using Tasks.Domain.Entities;

namespace Tasks.Application.Mappings
{
    public static class TaskMapping
    {
        public static TaskDto ToDto(this TaskEntity entity)
        {
            return new TaskDto
            {
                Id = entity.Id,
                CreatedDate = entity.CreatedDate,
                Name = entity.Name.Name,
                Description = entity.Description.Description,
                Status = new TaskStatusDto
                {
                    Name = entity.Status.Value,
                    Description = entity.Status.Description
                },
                //Target = entity.Target
            };
        }
    }
}
