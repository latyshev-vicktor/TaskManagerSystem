using MediatR;

namespace AuthenticationService.Domain.SeedWork
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedDate { get; private set; }
        public DateTimeOffset CreatedDate { get; } = DateTimeOffset.Now;

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
