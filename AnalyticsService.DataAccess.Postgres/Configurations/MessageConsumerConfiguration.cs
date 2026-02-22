using AnalyticsService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyticsService.DataAccess.Postgres.Configurations
{
    public class MessageConsumerConfiguration : IEntityTypeConfiguration<MessageConsumerEntity>
    {
        public void Configure(EntityTypeBuilder<MessageConsumerEntity> builder)
        {
            builder.ToTable("MessageConsumers");
            builder.HasKey(x => new { x.EventId, x.ConsumerName });
        }
    }
}
