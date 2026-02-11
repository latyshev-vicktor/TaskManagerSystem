namespace TaskManagerSystem.Common.Contracts.Events.Analytics.v1
{
    public record TaskStatusChangedEvent(
        Guid EventId,
        DateTimeOffset OccurredAt,
        Guid TaskId,
        Guid SprintId,
        Guid UserId,
        string Status) : IIntegrationEvent
    {
        public int Version => 1;
    }
}
