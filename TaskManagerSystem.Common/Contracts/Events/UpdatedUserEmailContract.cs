namespace TaskManagerSystem.Common.Contracts.Events
{
    public class UpdatedUserEmailContract
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}
