using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Tasks.Domain.Entities;

namespace Tasks.DataAccess.Postgres
{
    public class TaskDbContext(DbContextOptions<TaskDbContext> options, IMediator mediator) : DbContext(options)
    {
        private readonly IMediator _mediator = mediator;

        public DbSet<SprintEntity> Sprints { get; set; }
        public DbSet<TargetEntity> Targets { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskDbContext).Assembly);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var resultSaveChanges = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEvents(cancellationToken);

            return resultSaveChanges;
        }

        private async Task PublishDomainEvents(CancellationToken cancellationToken)
        {
            var domainEventsFromEntity = ChangeTracker.Entries<BaseEntity>()
                                                      .Select(entry => entry.Entity)
                                                      .SelectMany(entity =>
                                                      {
                                                          var domainEvents = entity.GetDomainEvents();

                                                          entity.ClearDomainEvents();
                                                           
                                                          return domainEvents;
                                                      });

            foreach (var domainEvent in domainEventsFromEntity)
                await _mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
