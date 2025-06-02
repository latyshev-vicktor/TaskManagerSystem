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
        ITokenGenerator tokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator) : IRequestHandler<LoginQuery, IExecutionResult<LoginResponse>>
    {
        public async Task<IExecutionResult<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users
                                      .AsNoTracking()
                                      .Include(x => x.Roles)
                                      .FirstOrDefaultAsync(UserSpecification.ByEmail(request.Email), cancellationToken);

            var accessToken = tokenGenerator.GenerateToken(user!);
            var refreshToken = await refreshTokenGenerator.GenerateAsync(user!.Id);

            return ExecutionResult.Success(new LoginResponse(accessToken, refreshToken, user.Id, user.UserName.Value, user.Email.Value, user.FullName.FirstName, user.FullName.LastName));
        }
    }
}
