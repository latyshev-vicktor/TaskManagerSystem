namespace TaskManagerSystem.Common.Contracts.Events
{
    public class UpdatedUserEmailContract
    {
        public long UserId { get; set; }
        public string Email { get; set; }
    }
}
