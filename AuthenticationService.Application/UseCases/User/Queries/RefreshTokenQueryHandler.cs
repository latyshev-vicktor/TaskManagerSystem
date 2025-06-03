using AuthenticationService.Application.Services;
using AuthenticationService.Application.UseCases.User.Dto;
using AuthenticationService.DataAccess.Postgres;
using AuthenticationService.Domain.Errors;
using AuthenticationService.Domain.Specification;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Application.UseCases.User.Queries
{
    public class RefreshTokenQueryHandler(
        ITokenService tokenService,
        AuthenticationDbContext dbContext) : IRequestHandler<RefreshTokenQuery, IExecutionResult<TokenResponse>>
    {
        public async Task<IExecutionResult<TokenResponse>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var validatedRefreshToken = await tokenService.ValidateRefreshTokenAsync(request.RefreshToken);
            if(!validatedRefreshToken)
                return ExecutionResult.Failure<TokenResponse>(UserError.UserByRefreshTokenNotFound());

            var userId = await tokenService.GetUserIdByRefreshToken(request.RefreshToken);
            if (userId == null)
                return ExecutionResult.Failure<TokenResponse>(UserError.UserByRefreshTokenNotFound());

            var user = await dbContext.Users
                                      .AsNoTracking()
                                      .AsSplitQuery()
                                      .Include(x => x.Roles)
                                        .ThenInclude(x => x.Permissions)
                                      .FirstOrDefaultAsync(UserSpecification.ById(userId.Value), cancellationToken);

            if (user == null)
                return ExecutionResult.Failure<TokenResponse>(UserError.UserByRefreshTokenNotFound());

            var (AccessToken, RefreshToken) = await tokenService.GenerateTokenAsync(user!);

            return ExecutionResult.Success(new TokenResponse(AccessToken, RefreshToken));
        }
    }
}
