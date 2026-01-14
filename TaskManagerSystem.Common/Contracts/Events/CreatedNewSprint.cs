namespace TaskManagerSystem.Common.Contracts.Events
{
    public record CreatedNewSprint(long SprintId, long UserId, string Name);
}
