namespace Tasks.Domain.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? DeletedDate { get; private set; }
        public DateTimeOffset CreatedDate { get; } = DateTimeOffset.Now;

        public void Delete()
        {
            IsDeleted = true;
            DeletedDate = DateTimeOffset.Now;
        }
    }
}
