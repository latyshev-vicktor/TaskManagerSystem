using NSpecifications;
using Tasks.Domain.Entities;

namespace Tasks.Domain.Specifications
{
    public static class TaskSpecification
    {
        public static Spec<TaskEntity> ById(Guid id)
            => new(x => x.Id == id);

        public static Spec<TaskEntity> ByTargetId(Guid targetId)
            => new(x => x.TargetId == targetId);
    }
}
