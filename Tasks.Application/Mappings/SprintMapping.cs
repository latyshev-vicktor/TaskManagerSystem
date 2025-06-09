using Tasks.Application.Dto;
using Tasks.Domain.Entities;

namespace Tasks.Application.Mappings
{
    public static class SprintMapping
    {
        public static SprintDto ToDto(this SprintEntity entity)
        {
            return new SprintDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                CreatedDate = entity.CreatedDate,
                Name = entity.Name.Name,
                Description = entity.Description.Description,
                SprintStatus = new SprintStatusDto
                {
                    Name = entity.Status.Value,
                    Description = entity.Status.Description,
                },
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                FieldActivities = [.. entity.SprintFieldActivities.Select(x => x.FieldActivity?.ToDto())],
                Targets = [.. entity.Targets.Select(x => x.ToDto())],
            };
        }
    }
}
