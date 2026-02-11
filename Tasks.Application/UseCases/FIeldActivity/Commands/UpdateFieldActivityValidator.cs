using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Errors;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Errors;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.FIeldActivity.Commands
{
    public class UpdateFieldActivityValidator : RequestValidator<UpdateFieldActivityCommand>
    {
        private readonly TaskDbContext _dbContext;
        public UpdateFieldActivityValidator(TaskDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Dto.Name).NotEmpty().WithMessage("Наименование сферы деятельности не может быть пустым");
            RuleFor(x => x.Dto.UserId).NotNull().NotEmpty().WithMessage("Пользователь не найден");
        }

        public async override Task<IExecutionResult> RequestValidateAsync(UpdateFieldActivityCommand request, CancellationToken cancellationToken)
        {
            var fieldActivity = await _dbContext.FieldActivities.FirstOrDefaultAsync(FieldActivitySpecification.ById(request.Dto.Id), cancellationToken);
            if (fieldActivity == null)
                return ExecutionResult.Failure(BaseEntityError.EntityNotFound("сфера деятельности"));

            if (fieldActivity.UserId != request.Dto.UserId)
                return ExecutionResult.Failure(FieldActivityError.NotBelongForCurrentUser());

            return ExecutionResult.Success();
        }
    }
}
