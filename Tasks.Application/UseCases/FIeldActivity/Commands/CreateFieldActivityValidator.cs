using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NSpecifications;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Entities;
using Tasks.Domain.Errors;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.FIeldActivity.Commands
{
    public class CreateFieldActivityValidator : RequestValidator<CreateFieldActivityCommand>
    {
        private readonly TaskDbContext _dbContext;
        public CreateFieldActivityValidator(TaskDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Name).NotEmpty().WithMessage("Наименование не может быть пустым");
            RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("Пользователь не найден");
        }

        public async override Task<IExecutionResult> RequestValidateAsync(CreateFieldActivityCommand request, CancellationToken cancellationToken)
        {
            var spec = Spec.Any<FieldActivityEntity>();
            spec &= FieldActivitySpecification.ByName(request.Name);
            spec &= FieldActivitySpecification.ByUserId(request.UserId);

            var existFieldActivityByName = await _dbContext.FieldActivities
                                                           .AsNoTracking()
                                                           .AnyAsync(spec, cancellationToken);
            if (existFieldActivityByName == true)
                return ExecutionResult.Failure(FieldActivityError.DublicateNameForCurrentUser());

            return ExecutionResult.Success();
        }
    }
}
