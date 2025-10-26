using AuthenticationService.Application.Services;
using AuthenticationService.Application.UseCases.User.Dto;
using AuthenticationService.DataAccess.Postgres;
using AuthenticationService.Domain.Specification;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Application.UseCases.User.Queries
{
    public class LoginQueryHandler(
        AuthenticationDbContext dbContext,
        ITokenService tokenService) : IRequestHandler<LoginQuery, IExecutionResult<LoginResponse>>
    {
        public async Task<IExecutionResult<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users
                                      .AsNoTracking()
                                      .Include(x => x.Roles)
                                      .FirstOrDefaultAsync(UserSpecification.ByEmail(request.Email), cancellationToken);

            var (AccessToken, RefreshToken) = await tokenService.GenerateTokenAsync(user!);

            return ExecutionResult.Success(new LoginResponse(AccessToken, RefreshToken));
        }
    }
}
