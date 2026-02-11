using NSpecifications;
using Tasks.Domain.Entities;

namespace Tasks.Domain.Specifications
{
    public static class FieldActivitySpecification
    {
        public static Spec<FieldActivityEntity> ByIds(Guid[] ids)
            => new(x => ids.Contains(x.Id));
        public static Spec<FieldActivityEntity> ById(Guid id)
            => new(x => x.Id == id);

        public static Spec<FieldActivityEntity> ByName(string name)
            => new(x => x.Name == name);

        public static Spec<FieldActivityEntity> ByUserId(Guid userId)
            => new(x => x.UserId == userId);
    }
}
