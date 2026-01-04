using AuthenticationService.DataAccess.Postgres;
using AuthenticationService.Domain.Errors;
using AuthenticationService.Domain.Specification;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Application.UseCases.User.Queries
{
    public class GetUserProfileInfoQueryValidator(AuthenticationDbContext dbContext) : RequestValidator<GetUserProfileInfoQuery>
    {
        public override async Task<IExecutionResult> RequestValidateAsync(GetUserProfileInfoQuery request, CancellationToken cancellationToken)
        {
            var existUser = await dbContext
                .Users
                .AsNoTracking()
                .Where(UserSpecification.ById(request.UserId))
                .AnyAsync(cancellationToken);

            if (!existUser)
                return ExecutionResult.Failure(UserError.UserByIdNotFound());

            return ExecutionResult.Success();
        }
    }
}
