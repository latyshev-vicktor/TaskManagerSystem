using Tasks.Domain.SeedWork;

namespace Tasks.Domain.Entities
{
    public class FieldActivityEntity : BaseEntity
    {
        public string Name { get; private set; }
        public Guid UserId { get; private set; }
        protected FieldActivityEntity()
        {
            
        }

        public FieldActivityEntity(string name, Guid userId)
        {
            Name = name;
            UserId = userId;
        }

        public void SetName(string name) => Name = name;
    }
}
