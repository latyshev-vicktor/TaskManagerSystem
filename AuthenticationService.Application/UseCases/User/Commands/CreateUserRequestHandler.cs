using AuthenticationService.Application.Services;
using AuthenticationService.DataAccess.Postgres;
using AuthenticationService.Domain.Entities;
using AuthenticationService.Domain.Errors;
using AuthenticationService.Domain.Specification;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Application.UseCases.User.Commands
{
    public class CreateUserRequestHandler(
        AuthenticationDbContext dbContext, 
        IPasswordHasher passwordHasher) : IRequestHandler<CreateUserRequest, IExecutionResult>
    {
        public async Task<IExecutionResult> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var existUserByEmail = await dbContext.Users.AnyAsync(UserSpecification.ByEmail(request.Dto.Email), cancellationToken);
            if (existUserByEmail == true)
                return ExecutionResult.Failure(UserError.DublicateEmailUser());

            var existUserByUserName = await dbContext.Users.AnyAsync(UserSpecification.ByUserName(request.Dto.UserName), cancellationToken);
            if (existUserByUserName == true)
                return ExecutionResult.Failure(UserError.DublicateUserName());

            var hasPassword = passwordHasher.GenerateHash(request.Dto.Password);

            var newUserResult = UserEntity.Create(request.Dto.UserName,
                                            request.Dto.FirstName,
                                            request.Dto.LastName,
                                            request.Dto.Email,
                                            request.Dto.Phone,
                                            hasPassword,
                                            request.Dto.BirthDay);


            if (newUserResult.IsFailure)
                return ExecutionResult.Failure(newUserResult.Error);

            await dbContext.Users.AddAsync(newUserResult.Value, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success();
        }
    }
}
