using MassTransit;
using System.Net.Mail;
using System.Net;
using TaskManagerSystem.Common.Contracts;
using Microsoft.Extensions.Configuration;

namespace AuthenticationService.Application.Consumers
{
    internal class SendEmailByCreatedNewUserConsumer(IConfiguration configuration) : IConsumer<CreatedNewUser>
    {
        public async Task Consume(ConsumeContext<CreatedNewUser> context)
        {
            using var smtpClient = new SmtpClient
            {
                Host = configuration["SmptSettings:Host"],
                Port = int.Parse(configuration["SmptSettings:Port"]),
                Credentials = new NetworkCredential(configuration["SmptSettings:Username"], configuration["SmptSettings:Password"]),
                EnableSsl = true
            };

            var body = $"Здравствуйте, {context.Message.FirstName} {context.Message.LastName}. Спасибо большое что выбрали нашу систему по планированию задач. Надеемся вы хороший котик и будете лапочкой. Всего наилучшего";

            var mailMassage = new MailMessage
            {
                From = new MailAddress(configuration["SmptSettings:Username"], "Task Management"),
                Subject = "Создание нового пользователя",
                Body = body,
                IsBodyHtml = true
            };

            mailMassage.To.Add(context.Message.Email);

            await smtpClient.SendMailAsync(mailMassage);
        }
    }
}
