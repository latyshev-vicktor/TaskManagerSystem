namespace TaskManagerSystem.Common.Contracts.Events
{
    public record InsightEvent(string Message, string SprintMessage, Guid UserId);
}
