using AuthenticationService.Application.UseCases.User.Dto;
using AuthenticationService.DataAccess.Postgres;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using TaskManagerSystem.Common.Interfaces;
using TaskManagerSystem.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using AuthenticationService.Domain.Specification;
using AuthenticationService.Application.Mappings;
using TaskManagerSystem.Common.Implementation;

namespace AuthenticationService.Application.UseCases.User.Queries
{
    public class GetShortInformationQueryHandler(
        AuthenticationDbContext dbContext,
        IDistributedCache cache) : IRequestHandler<GetShortInformationQuery, IExecutionResult<UserShortDto>>
    {
        public async Task<IExecutionResult<UserShortDto>> Handle(GetShortInformationQuery request, CancellationToken cancellationToken)
        {
            var userKey = $"userShort:{request.UserId}";
            var user = await cache.GetOrAddAsync(
                userKey, async () =>
                {
                    return await dbContext.Users
                                          .AsNoTracking()
                                          .Where(UserSpecification.ById(request.UserId))
                                          .Select(x => x.ToShortDto())
                                          .FirstOrDefaultAsync(cancellationToken);
                }, TimeSpan.FromMinutes(60));

            return ExecutionResult.Success(user!);
        }
    }
}
