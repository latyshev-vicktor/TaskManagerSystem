namespace TaskManagerSystem.Common.Contracts.Events.Analytics.v1
{
    public record CreatedNewSprint(
        Guid EventId,
        DateTimeOffset OccurredAt,
        Guid SprintId,
        Guid UserId,
        string Name) : IIntegrationEvent
    {
        public int Version => 1;
    }
}
