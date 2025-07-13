using FluentValidation;
using TaskManagerSystem.Common.Implementation;

namespace Tasks.Application.UseCases.FIeldActivity.Queires
{
    public class GetFieldActivitiesBySprintQueryValidator : RequestValidator<GetFieldActivitiesBySprintQuery>
    {
        public GetFieldActivitiesBySprintQueryValidator()
        {
            RuleFor(x => x.UserId).NotEqual(0).WithMessage("Идентификатор пользователя не может быть пустым");
            RuleFor(x => x.SprintId).NotEqual(0).WithMessage("Идентификатор пользователя не может быть пустым");
        }
    }
}
