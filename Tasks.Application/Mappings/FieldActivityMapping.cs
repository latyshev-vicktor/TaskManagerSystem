using Tasks.Application.Dto;
using Tasks.Domain.Entities;

namespace Tasks.Application.Mappings
{
    public static class FieldActivityMapping
    {
        public static FieldActivityDto ToDto(this FieldActivityEntity entity)
        {
            return new FieldActivityDto
            {
                Id = entity.Id,
                CreatedDate = entity.CreatedDate,
                Name = entity.Name,
                UserId = entity.UserId
            };
        }
    }
}
