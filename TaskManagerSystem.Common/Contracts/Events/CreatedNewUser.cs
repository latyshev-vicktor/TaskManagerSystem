namespace TaskManagerSystem.Common.Contracts.Events
{
    public class CreatedNewUser
    {
        public long UserId { get; set; }
        public string Email { get; set; }
    }
}
