namespace Notification.Application.Dto
{
    public class UserNotificationProfileDto : BaseDto
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public bool EnableEmail { get; set; }
        public bool EnableSignalR { get; set; }
    }
}
