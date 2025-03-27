using AuthenticationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationService.DataAccess.Postgres.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
    {
        public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
        {
            builder.ToTable("Role_Permission");
            builder.HasKey(x => new { x.RoleId, x.PermissionId });
        }
    }
}
