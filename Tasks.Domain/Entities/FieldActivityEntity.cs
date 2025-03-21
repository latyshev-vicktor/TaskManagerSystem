namespace Tasks.Domain.Entities
{
    public class FieldActivityEntity : BaseEntity
    {
        public string Name { get; private set; }
        public long UserId { get; private set; }
        protected FieldActivityEntity()
        {
            
        }

        public FieldActivityEntity(string name, long userId)
        {
            Name = name;
            UserId = userId;
        }

        public void SetName(string name) => Name = name;
    }
}
