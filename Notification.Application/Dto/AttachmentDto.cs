namespace Notification.Application.Dto
{
    public class AttachmentDto
    {
        public string Name { get; set; }
        public string MimeType { get; set; }
        public byte[] Content { get; set; }
    }
}
