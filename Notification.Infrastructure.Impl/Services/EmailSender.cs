using Notification.Application.Dto;
using Notification.Application.Services;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using Notification.Application.Options;

namespace Notification.Infrastructure.Impl.Services
{
    public class EmailSender(IOptions<SmptSetting> options) : IEmailSender
    {
        private readonly SmptSetting _smptSettings = options.Value;

        public async Task SendEmail(NotificationDto notification)
        {
            try
            {
                using var smtpClient = new SmtpClient
                {
                    Host = _smptSettings.Host,
                    Port = _smptSettings.Port,
                    Credentials = new NetworkCredential(_smptSettings.Username, _smptSettings.Password),
                    EnableSsl = true
                };

                var mailMassage = new MailMessage
                {
                    From = new MailAddress(_smptSettings.Username, "Task Management"),
                    Subject = notification.Title,
                    Body = notification.Message,
                    IsBodyHtml = true
                };

                mailMassage.To.Add(notification.Recivier);

                foreach(var attachment in notification.Attachments)
                {
                    using var memStream = new MemoryStream(attachment.Content);
                    mailMassage.Attachments.Add(new Attachment(memStream, attachment.Name, attachment.MimeType));
                }

                await smtpClient.SendMailAsync(mailMassage);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка при отправке сообщения по электронной почте: {ex.Message}", ex.InnerException);
            }
        }
    }
}
