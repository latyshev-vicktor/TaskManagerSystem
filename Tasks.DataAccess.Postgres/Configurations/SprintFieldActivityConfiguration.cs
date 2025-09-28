using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.Entities;

namespace Tasks.DataAccess.Postgres.Configurations
{
    public class SprintFieldActivityConfiguration : IEntityTypeConfiguration<SprintFieldActivityEntity>
    {
        public void Configure(EntityTypeBuilder<SprintFieldActivityEntity> builder)
        {
            builder.ToTable("Sprint_FieldActivities");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Sprint)
                   .WithMany(x => x.SprintFieldActivities)
                   .HasForeignKey(x => x.SprintId);

            builder.HasOne(x => x.FieldActivity)
                   .WithMany()
                   .HasForeignKey(x => x.FieldActivityId);
        }
    }
}
