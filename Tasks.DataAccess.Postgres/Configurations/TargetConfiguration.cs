using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.Entities;

namespace Tasks.DataAccess.Postgres.Configurations
{
    public class TargetConfiguration : BaseConfiguration<TargetEntity>
    {
        protected override void Config(EntityTypeBuilder<TargetEntity> builder)
        {
            builder.ToTable("Targets");

            ConfigureValueObjects(builder);

            builder.HasMany(x => x.Tasks)
                   .WithOne(x => x.Target)
                   .HasForeignKey(x => x.TargetId);

            var navigation = builder.Metadata.FindNavigation(nameof(TargetEntity.Tasks));
            navigation!.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureValueObjects(EntityTypeBuilder<TargetEntity> builder)
        {
            builder.ComplexProperty(x => x.Name, options =>
            {
                options.IsRequired()
                       .Property(x => x.Name)
                       .HasColumnName("Name");
            });
        }
    }
}
