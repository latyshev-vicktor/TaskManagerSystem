using AuthenticationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationService.DataAccess.Postgres.Configurations
{
    public class RoleUserConfiguration : IEntityTypeConfiguration<RoleUserEntity>
    {
        public void Configure(EntityTypeBuilder<RoleUserEntity> builder)
        {
            builder.ToTable("Role_User");
            builder.HasKey(x => new { x.RoleId, x.UserId });
        }
    }
}
