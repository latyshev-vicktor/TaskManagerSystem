using Microsoft.EntityFrameworkCore;
using Tasks.Domain.Entities;

namespace Tasks.DataAccess.Postgres
{
    public class TaskDbContext : DbContext
    {
        public DbSet<SprintEntity> Sprints { get; set; }
        public DbSet<TargetEntity> Targets { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<SprintStatusEntity> SprintStatuses { get; set; }
        public DbSet<TaskStatusEntity> TaskStatuses { get; set; }

        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskDbContext).Assembly);
        }
    }
}
