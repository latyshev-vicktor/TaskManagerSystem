using AnalyticsService.Domain.Entities;
using AnalyticsService.Domain.Entities.AnalitycsModels;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.DataAccess.Postgres
{
    public class AnalyticsDbContext(DbContextOptions<AnalyticsDbContext> options) : DbContext(options)
    {
        public DbSet<SprintAnalyticsEntity> SprintAnalitycs { get; set; } = null!;
        public DbSet<SprintTaskAnalyticsEntity> SprintTaskAnalytics { get; set; } = null!;
        public DbSet<InsightEntity> Insights { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnalyticsDbContext).Assembly);
        }
    }
}
