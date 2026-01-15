using AnalyticsService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyticsService.DataAccess.Postgres.Configurations
{
    public class InsightConfiguration : IEntityTypeConfiguration<InsightEntity>
    {
        public void Configure(EntityTypeBuilder<InsightEntity> builder)
        {
            builder.ToTable("Insights");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Severity).HasConversion<int>().IsRequired();
            builder.Property(x => x.Type).HasConversion<int>().IsRequired();
            builder.Property(x => x.Confidence).IsRequired();
            builder.Property(x => x.SprintId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Message).IsRequired();

            builder.HasIndex(x => new { x.SprintId, x.UserId, x.Type });
        }
    }
}