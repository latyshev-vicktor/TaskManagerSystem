using AuthenticationService.Application.UseCases.User.Dto;
using AuthenticationService.DataAccess.Postgres;
using AuthenticationService.Domain.Specification;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Application.UseCases.User.Queries
{
    public class GetUserProfileInfoQueryHandler(AuthenticationDbContext dbContext) : IRequestHandler<GetUserProfileInfoQuery, IExecutionResult<UserProfileDto>>
    {
        public async Task<IExecutionResult<UserProfileDto>> Handle(GetUserProfileInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await dbContext
                .Users
                .AsNoTracking()
                .Where(UserSpecification.ById(request.UserId))
                .Select(x => new UserProfileDto
                {
                    Id = x.Id,
                    FirstName = x.FullName.FirstName,
                    LastName = x.FullName.LastName,
                    Email = x.Email.Value,
                    Phone = x.Phone.PhoneNumber,
                    UserName = x.UserName.Value,
                    BirthDay = x.BirthDay
                }).FirstOrDefaultAsync(cancellationToken);

            return ExecutionResult.Success(user!);
        }
    }
}
