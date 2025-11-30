using NSpecifications;
using Tasks.Domain.Entities;

namespace Tasks.Domain.Specifications
{
    public static class TaskSpecification
    {
        public static Spec<TaskEntity> ById(long id)
            => new(x => x.Id == id);

        public static Spec<TaskEntity> ByTargetId(long targetId)
            => new(x => x.TargetId == targetId);
    }
}
