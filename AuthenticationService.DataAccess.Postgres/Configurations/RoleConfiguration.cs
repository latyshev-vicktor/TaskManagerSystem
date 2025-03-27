using AuthenticationService.Domain.Entities;
using AuthenticationService.Domain.ValueObjects.Role;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationService.DataAccess.Postgres.Configurations
{
    public class RoleConfiguration : BaseConfiguration<RoleEntity>
    {
        protected override void Config(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("Roles");

            builder.HasMany(x => x.Permissions)
                   .WithMany(x => x.Roles)
                   .UsingEntity<RolePermissionEntity>(
                        l => l.HasOne(x => x.Permission).WithMany().HasForeignKey(x => x.PermissionId),
                        r => r.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId),
                        k => k.HasKey(x => new { x.RoleId, x.PermissionId })
                   );

            ConfigureValueObjects(builder);
        }

        private void ConfigureValueObjects(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ComplexProperty(x => x.Name, options =>
            {
                options.IsRequired()
                       .Property(x => x.Name)
                       .IsRequired()
                       .HasColumnName(nameof(RoleEntity.Name))
                       .HasMaxLength(RoleName.MAX_NAME_LENGTH);
            });

            builder.ComplexProperty(x => x.Description, options =>
            {
                options.IsRequired()
                       .Property(x => x.Description)
                       .IsRequired()
                       .HasColumnName(nameof(RoleEntity.Description));
            });
        }
    }
}
