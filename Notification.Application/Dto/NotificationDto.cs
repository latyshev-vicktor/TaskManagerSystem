namespace Notification.Application.Dto
{
    public class NotificationDto : BaseDto
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public long UserId { get; set; }
        public DateTimeOffset? ReadDate { get; set; }
    }
}
