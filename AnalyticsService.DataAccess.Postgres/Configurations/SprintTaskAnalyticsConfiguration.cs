using AnalyticsService.Domain.Entities.AnalitycsModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyticsService.DataAccess.Postgres.Configurations
{
    public class SprintTaskAnalyticsConfiguration : IEntityTypeConfiguration<SprintTaskAnalyticsEntity>
    {
        public void Configure(EntityTypeBuilder<SprintTaskAnalyticsEntity> builder)
        {
            builder.ToTable("SprintTaskAnalytics");
            builder.HasKey(e => e.TaskId);
            builder.Property(x => x.Status).HasConversion<int>().IsRequired();
        }
    }
}
