using AuthenticationService.DataAccess.Postgres;
using AuthenticationService.Domain.Specification;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Application.UseCases.User.Commands
{
    public class UpdateUserCommandHandler(AuthenticationDbContext dbContext, IDistributedCache cache) : IRequestHandler<UpdateUserCommand, IExecutionResult>
    {
        public async Task<IExecutionResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(UserSpecification.ById(request.Dto.Id), cancellationToken);

            user!.SetPhone(request.Dto.Phone);
            user.SetUserName(request.Dto.UserName);
            user.SetFullName(request.Dto.FirstName, request.Dto.LastName);
            user.SetEmail(request.Dto.Email);
            user.SetBirthDay(request.Dto.BirthDay);

            await dbContext.SaveChangesAsync(cancellationToken);
            var cacheKey = $"userShort:{user.Id}";
            cache.Remove(cacheKey);

            return ExecutionResult.Success();
        }
    }
}
