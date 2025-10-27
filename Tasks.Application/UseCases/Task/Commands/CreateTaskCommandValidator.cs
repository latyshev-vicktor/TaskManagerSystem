using FluentValidation;
using TaskManagerSystem.Common.Extensions;
using TaskManagerSystem.Common.Implementation;
using Tasks.Domain.Errors;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.UseCases.Task.Commands
{
    public class CreateTaskCommandValidator : RequestValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.CreateDto.Name).MustBeValueObject(TaskName.Create);
            RuleFor(x => x.CreateDto.Description).MustBeValueObject(TaskDescription.Create);
            RuleFor(x => x.CreateDto.TargetId).NotEqual(default(long)).CustomErrorMessage(TaskError.TargetIdNotFound());
        }
    }
}
