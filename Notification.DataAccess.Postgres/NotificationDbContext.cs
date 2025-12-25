using MediatR;
using Microsoft.EntityFrameworkCore;
using Notification.Domain.Entities;
using Notification.Domain.SeedWork;

namespace Notification.DataAccess.Postgres
{
    public class NotificationDbContext(DbContextOptions<NotificationDbContext> options, IMediator mediator) : DbContext(options)
    {
        public DbSet<NotificationEntity> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result =await base.SaveChangesAsync(cancellationToken);
            await PublishDomainEvents(cancellationToken);

            return result;
        }

        private async Task PublishDomainEvents(CancellationToken cancellationToken)
        {
            var domainEntites = ChangeTracker.Entries<BaseEntity>()
                .Where(x => x.Entity.GetDomainEvents().Any())
                .Select(x => x.Entity)
                .ToList();

            var domainEvents = domainEntites.SelectMany(x => x.GetDomainEvents())
                                            .ToList();

            domainEntites.ToList().ForEach(x =>
            {
                x.ClearDomainEvents();
            });

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
