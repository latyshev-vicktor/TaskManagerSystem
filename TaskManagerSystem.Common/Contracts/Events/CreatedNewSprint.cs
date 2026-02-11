namespace TaskManagerSystem.Common.Contracts.Events
{
    public record CreatedNewSprint(Guid SprintId, Guid UserId, string Name);
}
