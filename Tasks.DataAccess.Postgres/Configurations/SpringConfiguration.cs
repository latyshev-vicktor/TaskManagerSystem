using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.Entities;
using Tasks.Domain.ValueObjects;

namespace Tasks.DataAccess.Postgres.Configurations
{
    public class SpringConfiguration : BaseConfiguration<SprintEntity>
    {
        protected override void Config(EntityTypeBuilder<SprintEntity> builder)
        {
            builder.ToTable("Sprints");

            ConfigureValueObjects(builder);

            builder.HasMany(x => x.Targets)
                   .WithOne(x => x.Sprint)
                   .HasForeignKey(x => x.SprintId);

            var navigation = builder.Metadata.FindNavigation(nameof(SprintEntity.Targets));
            navigation!.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureValueObjects(EntityTypeBuilder<SprintEntity> builder)
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
                       .HasColumnName("Description")
                       .HasMaxLength(SprintDescription.MAX_LENGTH_DESCRIPTION);
            });
        }
    }
}
