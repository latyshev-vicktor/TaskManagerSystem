namespace TaskManagerSystem.Common.Contracts.Events
{
    public class SprintChangedStatus
    {
        public string SprintName { get; set; }
        public string Status { get; set; }
        public long UserId { get; set; }
    }
}
