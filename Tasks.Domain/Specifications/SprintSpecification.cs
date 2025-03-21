using NSpecifications;
using Tasks.Domain.Entities;

namespace Tasks.Domain.Specifications
{
    public static class SprintSpecification
    {
        public static Spec<SprintEntity> ById(long id)
            => new(x => x.Id == id);

        public static Spec<SprintEntity> ByUserId(long userId)
            => new(x => x.UserId == userId);

        public static Spec<SprintEntity> ByStatus(string statusName)
            => new(x => x.Status.Value == statusName);

        public static Spec<SprintEntity> ByFieldActivity(long fieldActivityId)
            => new(x => x.FieldActivityId == fieldActivityId);
    }
}
