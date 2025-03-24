using AuthenticationService.Domain.Entities;
using AuthenticationService.Domain.ValueObjects.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationService.DataAccess.Postgres.Configurations
{
    public class UserConfiguration : BaseConfiguration<UserEntity>
    {
        protected override void Config(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");

            builder.HasMany(x => x.Roles)
                   .WithMany(x => x.Users)
                   .UsingEntity<RoleUserEntity>(
                        l => l.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId),
                        r => r.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId),
                        k => k.HasKey(x => new { x.RoleId, x.UserId })
                   );

            ConfigureValueObjects(builder);
        }

        private void ConfigureValueObjects(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ComplexProperty(x => x.UserName, options =>
            {
                options.IsRequired()
                       .Property(x => x.Value)
                       .IsRequired()
                       .HasColumnName(nameof(UserEntity.UserName))
                       .HasMaxLength(UserName.USER_NAME_MAX_LENGHT);
            });

            builder.ComplexProperty(x => x.Phone, options =>
            {
                options.IsRequired()
                       .Property(x => x.PhoneNumber)
                       .IsRequired()
                       .HasColumnName(nameof(UserEntity.Phone))
                       .HasMaxLength(Phone.MAX_LENGHT_PHONE_NUMBER);
            });

            builder.ComplexProperty(x => x.FullName, options =>
            {
                options.IsRequired()
                       .Property(x => x.FirstName)
                       .IsRequired()
                       .HasColumnName(nameof(FullName.FirstName))
                       .HasMaxLength(FullName.MAX_LENGHT_FIRST_NAME);

                options.Property(x => x.LastName)
                       .IsRequired()
                       .HasColumnName(nameof(FullName.LastName))
                       .HasMaxLength(FullName.MAX_LENGHT_LAST_NAME);
            });

            builder.ComplexProperty(x => x.Email, options =>
            {
                options.IsRequired()
                       .Property(x => x.Value)
                       .IsRequired()
                       .HasColumnName(nameof(UserEntity.Email));
            });
        }
    }
}
