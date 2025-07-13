using NSpecifications;
using Tasks.Domain.Entities;

namespace Tasks.Domain.Specifications
{
    public static class SprintFieldActivitySpecification
    {
        public static Spec<SprintFieldActivityEntity> ById(long id)
            => new(x => x.Id == id);

        public static Spec<SprintFieldActivityEntity> BySprintId(long sprintId)
            => new(x => x.SprintId == sprintId);
    }
}
