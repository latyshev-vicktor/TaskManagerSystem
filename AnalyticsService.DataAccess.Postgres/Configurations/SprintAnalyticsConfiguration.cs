using AnalyticsService.Domain.Entities.AnalitycsModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyticsService.DataAccess.Postgres.Configurations
{
    public class SprintAnalyticsConfiguration : IEntityTypeConfiguration<SprintAnalyticsEntity>
    {
        public void Configure(EntityTypeBuilder<SprintAnalyticsEntity> builder)
        {
            builder.ToTable("SprintAnalytics");
            builder.HasKey(x => x.SprintId);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Name).IsRequired();

            builder.HasMany(x => x.Tasks)
                .WithOne(x => x.Sprint)
                .HasForeignKey(x => x.SprintId);

            builder.HasIndex(x => new { x.UserId, x.SprintId });
        }
    }
}
