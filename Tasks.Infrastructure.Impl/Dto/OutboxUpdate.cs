namespace Tasks.Infrastructure.Impl.Dto
{
    internal class OutboxUpdate
    {
        public Guid Id { get; set; }
        public DateTime? ProcessedOnUtc { get; set; }
        public string? Error { get; set; }
    }
}
