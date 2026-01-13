namespace TaskManagerSystem.Common.Contracts.Events.Analytics
{
    public interface IIntegrationEvent
    {
        Guid EventId { get; }
        /// <summary>
        /// Дата когда произошел event
        /// </summary>
        DateTimeOffset OccurredAt { get; }
        int Version { get; }
    }
}
