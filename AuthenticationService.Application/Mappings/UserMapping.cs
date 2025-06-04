using AuthenticationService.Application.UseCases.User.Dto;
using AuthenticationService.Domain.Entities;

namespace AuthenticationService.Application.Mappings
{
    public static class UserMapping
    {
        public static UserShortDto ToShortDto(this UserEntity entity)
        {
            return new UserShortDto
            {
                UserId = entity.Id,
                UserName = entity.UserName.Value,
                Email = entity.Email.Value,
                FirstName = entity.FullName.FirstName,
                LastName = entity.FullName.LastName,
                CreatedDate = entity.CreatedDate
            };
        }
    }
}
