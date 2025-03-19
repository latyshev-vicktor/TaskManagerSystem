using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Tasks.Domain.Entities;

namespace Tasks.DataAccess.Postgres
{
    public class TaskDbContext : DbContext
    {
        private readonly IMediator _mediator;

        public DbSet<SprintEntity> Sprints { get; set; }
        public DbSet<TargetEntity> Targets { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<SprintStatusEntity> SprintStatuses { get; set; }
        public DbSet<TaskStatusEntity> TaskStatuses { get; set; }

        public TaskDbContext(DbContextOptions<TaskDbContext> options, IMediator mediator) : base(options) 
        {
            _mediator = mediator;
        }

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
