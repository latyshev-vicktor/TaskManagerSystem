using AuthenticationService.Domain.Entities;
using AuthenticationService.Domain.ValueObjects.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationService.DataAccess.Postgres.Configurations
{
    public class PermissionConfiguration : BaseConfiguration<PermissionEntity>
    {
        protected override void Config(EntityTypeBuilder<PermissionEntity> builder)
        {
            builder.ToTable("Permissions");

            ConfigureValueObject(builder);
        }

        private void ConfigureValueObject(EntityTypeBuilder<PermissionEntity> builder)
        {
            builder.ComplexProperty(x => x.Name, options =>
            {
                options.IsRequired()
                       .Property(x => x.Name)
                       .HasMaxLength(PermissionName.MAX_NAME_LENGTH)
                       .HasColumnName(nameof(PermissionEntity.Name));

            });

            builder.ComplexProperty(x => x.Description, options =>
            {
                options.IsRequired()
                       .Property(x => x.Description)
                       .HasColumnName(nameof(PermissionEntity.Description));
            });
        }
    }
}
