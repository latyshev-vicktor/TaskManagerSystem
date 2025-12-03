using NSpecifications;
using Tasks.Domain.Entities;

namespace Tasks.Domain.Specifications
{
    public static class SprintWeekSpecification
    {
        public static Spec<SprintWeekEntity> ById(long id)
            => new(x => x.Id == id);

        public static Spec<SprintWeekEntity> BySprintId(long sprintId)
            => new(x => x.SprintId == sprintId);
    }
}
