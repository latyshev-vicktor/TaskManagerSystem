using AuthenticationService.Application.Services;
using AuthenticationService.Domain.Errors;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Application.UseCases.User.Queries
{
    public class RefreshTokenQueryValidator(IRefreshTokenGenerator refreshTokenGenerator) : RequestValidator<RefreshTokenQuery>
    {
        public override async Task<IExecutionResult> RequestValidateAsync(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var userId = await refreshTokenGenerator.GetUserIdByTokenAsync(request.RefreshToken);
            if (userId == null)
                return ExecutionResult.Failure(UserError.UserByRefreshTokenNotFound());

            return ExecutionResult.Success();
        }
    }
}
