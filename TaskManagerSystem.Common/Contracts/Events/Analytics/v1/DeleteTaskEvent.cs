namespace TaskManagerSystem.Common.Contracts.Events.Analytics.v1
{
    public record DeleteTaskEvent(
        Guid EventId,
        DateTimeOffset OccurredAt,
        Guid TaskId,
        Guid SprintId) : IIntegrationEvent
    {
        public int Version => 1;
    }
}
