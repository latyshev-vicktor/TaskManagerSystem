using AuthenticationService.Domain.Entities;
using AuthenticationService.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.DataAccess.Postgres
{
    public class AuthenticationDbContext : DbContext
    {
        private readonly IMediator _mediator;

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<RoleUserEntity> RoleUsers { get; set; }
        public DbSet<RolePermissionEntity> RolePermissions { get; set; }

        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthenticationDbContext).Assembly);
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
