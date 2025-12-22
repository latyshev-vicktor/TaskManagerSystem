using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.Notification.Queries
{
    public record GetUnreadCountQuery(long UserId) : IRequest<IExecutionResult<long>>;
}
