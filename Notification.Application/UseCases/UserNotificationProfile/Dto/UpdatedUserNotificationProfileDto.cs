namespace Notification.Application.UseCases.UserNotificationProfile.Dto
{
    public class UpdatedUserNotificationProfileDto
    {
        public Guid Id { get; set; }
        public long UserId { get; set; }
        public bool EnableEmail { get; set; }
        public bool EnableSignalR { get; set; }
    }
}
