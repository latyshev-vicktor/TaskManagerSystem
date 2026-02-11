using NSpecifications;
using Tasks.Domain.Entities;

namespace Tasks.Domain.Specifications
{
    public static class SprintFieldActivitySpecification
    {
        public static Spec<SprintFieldActivityEntity> ById(Guid id)
            => new(x => x.Id == id);

        public static Spec<SprintFieldActivityEntity> BySprintId(Guid sprintId)
            => new(x => x.SprintId == sprintId);
    }
}
