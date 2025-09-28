using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.Entities;

namespace Tasks.DataAccess.Postgres.Configurations
{
    public class SprintWeekConfiguration : BaseConfiguration<SprintWeekEntity>
    {
        protected override void Config(EntityTypeBuilder<SprintWeekEntity> builder)
        {
            builder.ToTable("SprintWeeks");

            builder.Property(x => x.SprintId).IsRequired();

            builder.HasMany(x => x.Tasks)
                   .WithOne(x => x.Week)
                   .HasForeignKey(x => x.WeekId);

            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
        }
    }
}
