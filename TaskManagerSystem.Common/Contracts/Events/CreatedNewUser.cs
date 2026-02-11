namespace TaskManagerSystem.Common.Contracts.Events
{
    public class CreatedNewUser
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}
