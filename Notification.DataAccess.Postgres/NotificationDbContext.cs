using Microsoft.EntityFrameworkCore;
using Notification.Domain.Entities;

namespace Notification.DataAccess.Postgres
{
    public class NotificationDbContext(DbContextOptions<NotificationDbContext> options) : DbContext(options)
    {
        public DbSet<NotificationEntity> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
        }
    }
}
