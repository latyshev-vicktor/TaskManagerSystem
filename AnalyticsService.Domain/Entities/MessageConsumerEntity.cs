namespace AnalyticsService.Domain.Entities
{
    public class MessageConsumerEntity
    {
        public Guid EventId { get; set; }
        public string ConsumerName { get; set; }
        public DateTime ConsumedAtUtc { get; set; }
    }
}
