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
        ITokenGenerator tokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator,
        AuthenticationDbContext dbContext) : IRequestHandler<RefreshTokenQuery, IExecutionResult<TokenResponse>>
    {
        public async Task<IExecutionResult<TokenResponse>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var userId = await refreshTokenGenerator.GetUserIdByTokenAsync(request.RefreshToken);

            var user = await dbContext.Users
                                      .AsNoTracking()
                                      .FirstOrDefaultAsync(UserSpecification.ById(userId.Value), cancellationToken);

            if (user == null)
                return ExecutionResult.Failure<TokenResponse>(UserError.UserByRefreshTokenNotFound());

            var accessToken = tokenGenerator.GenerateToken(user);
            var refreshToken = await refreshTokenGenerator.GenerateAsync(userId.Value);

            return ExecutionResult.Success(new TokenResponse(accessToken, refreshToken));
        }
    }
}
