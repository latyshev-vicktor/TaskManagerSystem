using NSpecifications;
using Tasks.Domain.Entities;

namespace Tasks.Domain.Specifications
{
    public static class SprintWeekSpecification
    {
        public static Spec<SprintWeekEntity> ById(Guid id)
            => new(x => x.Id == id);

        public static Spec<SprintWeekEntity> BySprintId(Guid sprintId)
            => new(x => x.SprintId == sprintId);
    }
}
