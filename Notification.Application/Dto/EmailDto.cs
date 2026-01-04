namespace Notification.Application.Dto
{
    public class EmailDto
    {
        public string Recivier { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public List<AttachmentDto> Attachments { get; set; } = [];
    }
}
