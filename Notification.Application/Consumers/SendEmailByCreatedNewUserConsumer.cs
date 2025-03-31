using MassTransit;
using Notification.Application.Dto;
using Notification.Application.Services;
using TaskManagerSystem.Common.Contracts;

namespace Notification.Application.Consumers
{
    public class SendEmailByCreatedNewUserConsumer(IEmailSender emailSender) : IConsumer<CreatedNewUser>
    {
        public async Task Consume(ConsumeContext<CreatedNewUser> context)
        {
            try
            {
                var notification = new NotificationDto
                {
                    Title = "Регистрация пользователя",
                    Message = $"Здравствуйте, {context.Message.FirstName} {context.Message.LastName}. Спасибо большое что выбрали нашу систему по планированию задач. Надеемся вы хороший котик и будете лапочкой. Всего наилучшего",
                    Recivier = context.Message.Email
                };

                await emailSender.SendEmail(notification);
            }
            catch (Exception ex)
            {
                await context.Redeliver(TimeSpan.FromMinutes(1));
            }
        }
    }
}
