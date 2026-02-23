namespace TaskManagerSystem.Common.Contracts.Events.Analytics.v1
{
    public record CreatedNewSprint(
        Guid SprintId,
        Guid UserId,
        Guid EventId,
        DateTimeOffset OccurredAt,
        string Name) : IIntegrationEvent
    {
        public int Version => 1;
    }
}
