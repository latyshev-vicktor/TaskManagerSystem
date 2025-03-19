using MediatR;

namespace Tasks.Domain.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? DeletedDate { get; private set; }
        public DateTimeOffset CreatedDate { get; } = DateTimeOffset.Now;

        private List<INotification> _domainEvents = [];
        public IReadOnlyList<INotification> GetDomainEvents() => _domainEvents;

        public void RiseDomainEvents(INotification domainEvent)
            => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents()
            => _domainEvents?.Clear();

        public void Delete()
        {
            IsDeleted = true;
            DeletedDate = DateTimeOffset.Now;
        }
    }
}
