using NSpecifications;
using Tasks.Domain.Entities;

namespace Tasks.Domain.Specifications
{
    public class TargetSpecification
    {
        public static Spec<TargetEntity> ById(Guid id)
            => new(x => x.Id == id);

        public static Spec<TargetEntity> BySprintId(Guid sprintId)
            => new(x => x.SprintId == sprintId);
    }
}
