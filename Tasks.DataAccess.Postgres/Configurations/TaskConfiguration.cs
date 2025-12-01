using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.Entities;

namespace Tasks.DataAccess.Postgres.Configurations
{
    public class TaskConfiguration : BaseConfiguration<TaskEntity>
    {
        protected override void Config(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.ToTable("Tasks");

            ConfigureValueObjects(builder);
        }

        private void ConfigureValueObjects(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.ComplexProperty(x => x.Status, options =>
            {
                options.IsRequired()
                    .Property(x => x.Value).HasColumnName("StatusName");

                options.IsRequired()
                    .Property(x => x.Description).HasColumnName("StatusDescription");
            });

            builder.ComplexProperty(x => x.Name, options =>
            {
                options.IsRequired()
                       .Property(x => x.Name)
                       .HasColumnName("Name");
            });

            builder.ComplexProperty(x => x.Description, options =>
            {
                options.IsRequired()
                       .Property(x => x.Description)
                       .HasColumnName("Description");
            });
        }
    }
}
