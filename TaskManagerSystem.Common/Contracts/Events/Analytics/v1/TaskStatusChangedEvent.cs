namespace TaskManagerSystem.Common.Contracts.Events.Analytics.v1
{
    public record TaskStatusChangedEvent(
        Guid EventId,
        DateTime OccurredAt,
        long TaskId,
        long SprintId,
        long UserId,
        string Status) : IIntegrationEvent
    {
        public int Version => 1;
    }
}
