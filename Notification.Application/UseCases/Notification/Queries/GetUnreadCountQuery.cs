using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.Notification.Queries
{
    public record GetUnreadCountQuery(Guid UserId) : IRequest<IExecutionResult<long>>;
}
