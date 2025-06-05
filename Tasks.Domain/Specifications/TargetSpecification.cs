using NSpecifications;
using Tasks.Domain.Entities;

namespace Tasks.Domain.Specifications
{
    public class TargetSpecification
    {
        public static Spec<TargetEntity> ById(long id)
            => new(x => x.Id == id);

        public static Spec<TargetEntity> BySprintId(long sprintId)
            => new(x => x.SprintId == sprintId);
    }
}
