using FluentValidation;
using TaskManagerSystem.Common.Extensions;
using TaskManagerSystem.Common.Implementation;
using Tasks.Domain.Errors;

namespace Tasks.Application.UseCases.FIeldActivity.Queires
{
    public class GetMyFieldActivitiesQueryValidator : RequestValidator<GetMyFieldActivitiesQuery>
    {
        public GetMyFieldActivitiesQueryValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().CustomErrorMessage(FieldActivityError.NotFoundUserId());
        }
    }
}
