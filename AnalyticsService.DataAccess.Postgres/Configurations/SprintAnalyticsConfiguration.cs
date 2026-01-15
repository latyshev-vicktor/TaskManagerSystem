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
            builder.HasKey(e => e.Id);
            builder.Property(x => x.UserId).IsUnicode();
            builder.Property(x => x.SprintId).IsRequired();
            builder.Property(x => x.Name).IsRequired();

            builder.HasIndex(x => new { x.UserId, x.SprintId });
        }
    }
}
