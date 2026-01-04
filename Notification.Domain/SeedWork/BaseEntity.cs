using MediatR;

namespace Notification.Domain.SeedWork
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? DeletedDate { get; private set; }
        public DateTimeOffset CreatedDate { get; } = DateTime.UtcNow;

        private List<INotification> _domainEvents = [];
        public IReadOnlyList<INotification> GetDomainEvents() => _domainEvents;

        public void RiseDomainEvents(INotification domainEvent)
            => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents()
            => _domainEvents?.Clear();

        public virtual void Delete()
        {
            IsDeleted = true;
            DeletedDate = DateTime.Now;
        }
    }
}
