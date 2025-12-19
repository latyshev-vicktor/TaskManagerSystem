namespace Notification.Domain.SeedWork
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedDate { get; private set; }
        public DateTime CreatedDate { get; } = DateTime.Now;
    }
}
