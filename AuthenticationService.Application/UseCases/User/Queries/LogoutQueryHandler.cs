using AuthenticationService.Application.Services;
using AuthenticationService.DataAccess.Postgres;
using AuthenticationService.Domain.Specification;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Errors;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Application.UseCases.User.Queries
{
    public class LogoutQueryHandler(AuthenticationDbContext dbContext, ITokenService tokenService) : IRequestHandler<LogoutQuery, IExecutionResult>
    {
        public async Task<IExecutionResult> Handle(LogoutQuery request, CancellationToken cancellationToken)
        {
            var existUser = await dbContext.Users.AsNoTracking()
                                                 .AnyAsync(UserSpecification.ById(request.UserId), cancellationToken);

            if (!existUser)
                return ExecutionResult.Failure(BaseEntityError.EntityNotFound("пользователь"));

            await tokenService.RevokeAllTokensForUser(request.UserId);

            return ExecutionResult.Success();
        }
    }
}
