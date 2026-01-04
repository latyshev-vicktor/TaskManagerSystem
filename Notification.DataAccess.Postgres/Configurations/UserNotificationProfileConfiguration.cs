using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Domain.Entities;

namespace Notification.DataAccess.Postgres.Configurations
{
    public class UserNotificationProfileConfiguration : BaseConfiguration<UserNotificationProfileEntity>
    {
        public override void Config(EntityTypeBuilder<UserNotificationProfileEntity> builder)
        {
            builder.ToTable("UserNotificationProfiles");

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.EnableEmail).HasDefaultValue(true).IsRequired();
            builder.Property(x => x.EnableSignalR).HasDefaultValue(true).IsRequired();

            builder.HasIndex(x => x.UserId);
        }
    }
}
