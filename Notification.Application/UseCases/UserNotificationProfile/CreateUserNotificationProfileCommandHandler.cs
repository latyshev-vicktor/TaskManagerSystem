using MediatR;
using Microsoft.EntityFrameworkCore;
using Notification.DataAccess.Postgres;
using Notification.Domain.Entities;
using Notification.Domain.Specifications;

namespace Notification.Application.UseCases.UserNotificationProfile
{
    public class CreateUserNotificationProfileCommandHandler(NotificationDbContext dbContext) : IRequestHandler<CreateUserNotificationProfileCommand>
    {
        public async Task Handle(CreateUserNotificationProfileCommand request, CancellationToken cancellationToken)
        {
            var existProfile = await dbContext.UserNotificationProfiles
                .AnyAsync(UserNotificationProfileSpecification.ByUserId(request.UserId), cancellationToken);

            if (existProfile)
                return;

            var newUserNotificationProfile = new UserNotificationProfileEntity(request.UserId, request.Email);
            await dbContext.UserNotificationProfiles.AddAsync(newUserNotificationProfile, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
