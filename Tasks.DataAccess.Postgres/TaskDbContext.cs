using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Tasks.Domain.Entities;
using Tasks.Domain.SeedWork;

namespace Tasks.DataAccess.Postgres
{
    public class TaskDbContext : DbContext
    {
        private readonly IMediator _mediator;

        public DbSet<SprintEntity> Sprints { get; set; }
        public DbSet<TargetEntity> Targets { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<FieldActivityEntity> FieldActivities { get; set; }
        public DbSet<SprintFieldActivityEntity> SprintFieldActivities { get; set; }
        public DbSet<SprintWeekEntity> SprintWeeks { get; set; }

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
                await _mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
