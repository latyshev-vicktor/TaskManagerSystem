using AuthenticationService.DataAccess.Postgres;
using AuthenticationService.Domain.Errors;
using AuthenticationService.Domain.Specification;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Extensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Application.UseCases.User.Queries
{
    public class GetShortInformationQueryValidator : RequestValidator<GetShortInformationQuery>
    {
        private readonly AuthenticationDbContext _dbContext;

        public GetShortInformationQueryValidator(AuthenticationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.UserId).NotNull().CustomErrorMessage(UserError.UserIdNotNull());
        }

        public override async Task<IExecutionResult> RequestValidateAsync(GetShortInformationQuery request, CancellationToken cancellationToken)
        {
            var existUserById = await _dbContext.Users
                                                .AsNoTracking()
                                                .Where(UserSpecification.ById(request.UserId))
                                                .AnyAsync(cancellationToken);

            if (existUserById == false)
                return ExecutionResult.Failure(UserError.UserByIdNotFound());

            return ExecutionResult.Success();
        }
    }
}
