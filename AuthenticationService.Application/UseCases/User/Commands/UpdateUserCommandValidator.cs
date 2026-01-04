using AuthenticationService.DataAccess.Postgres;
using AuthenticationService.Domain.Errors;
using AuthenticationService.Domain.Specification;
using AuthenticationService.Domain.ValueObjects.User;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Extensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Application.UseCases.User.Commands
{
    public class UpdateUserCommandValidator : RequestValidator<UpdateUserCommand>
    {
        private readonly AuthenticationDbContext _dbContext;
        public UpdateUserCommandValidator(AuthenticationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Dto.UserName).MustBeValueObject(UserName.Create);
            RuleFor(x => x.Dto).Custom((field, context) =>
            {
                var fullNameResult = FullName.Create(field.FirstName, field.LastName);
                if (fullNameResult.IsSuccess)
                    return;

                context.AddFailure(fullNameResult.Error.Message);
            });
            RuleFor(x => x.Dto.Email).MustBeValueObject(Email.Create);
            RuleFor(x => x.Dto.Phone).MustBeValueObject(Phone.Create);
        }

        public async override Task<IExecutionResult> RequestValidateAsync(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existUser = await _dbContext.Users
                .AsNoTracking()
                .AnyAsync(UserSpecification.ById(request.Dto.Id), cancellationToken);

            if (!existUser)
                return ExecutionResult.Failure(UserError.UserByIdNotFound());

            return ExecutionResult.Success();
        }
    }
}
