namespace Notification.Application.Dto
{
    public abstract class BaseDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
