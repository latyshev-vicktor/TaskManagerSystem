using MediatR;

namespace Notification.Domain.SeedWork
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedDate { get; private set; }
        public DateTime CreatedDate { get; } = DateTime.Now;

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
