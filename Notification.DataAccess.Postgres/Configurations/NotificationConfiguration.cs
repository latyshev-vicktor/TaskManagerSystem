using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Domain.Entities;

namespace Notification.DataAccess.Postgres.Configurations
{
    public class NotificationConfiguration : BaseConfiguration<NotificationEntity>
    {
        public override void Config(EntityTypeBuilder<NotificationEntity> builder)
        {
            builder.ToTable("Notifications");

            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Message).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.IsRead).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.ReadDate).IsRequired(false).HasDefaultValue(null);
        }
    }
}
