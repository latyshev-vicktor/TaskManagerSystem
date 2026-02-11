using FluentValidation;
using TaskManagerSystem.Common.Implementation;

namespace Tasks.Application.UseCases.FIeldActivity.Queires
{
    public class GetFieldActivitiesBySprintQueryValidator : RequestValidator<GetFieldActivitiesBySprintQuery>
    {
        public GetFieldActivitiesBySprintQueryValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("Идентификатор пользователя не может быть пустым");
            RuleFor(x => x.SprintId).NotNull().NotEmpty().WithMessage("Идентификатор пользователя не может быть пустым");
        }
    }
}
