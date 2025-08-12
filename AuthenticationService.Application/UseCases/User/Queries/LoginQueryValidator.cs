using AuthenticationService.Application.Services;
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
    public class LoginQueryValidator : RequestValidator<LoginQuery>
    {
        private readonly AuthenticationDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;
        public LoginQueryValidator(AuthenticationDbContext dbContext, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;

            RuleFor(x => x.Email).NotEmpty().NotNull().CustomErrorMessage(UserError.EmailNotBeEmpty());
            RuleFor(x => x.Password).NotEmpty().NotNull().CustomErrorMessage(UserError.PasswordNotBeEmpty());
        }

        public override async Task<IExecutionResult> RequestValidateAsync(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(UserSpecification.ByEmail(request.Email), cancellationToken);

            if (user == null)
                return ExecutionResult.Failure(UserError.NotFoundByEmailOrPassword());

            var verifyPassword = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);
            if (verifyPassword == false)
                return ExecutionResult.Failure(UserError.NotFoundByEmailOrPassword());

            return ExecutionResult.Success();
        }
    }
}
