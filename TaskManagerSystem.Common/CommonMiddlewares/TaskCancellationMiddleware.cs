using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TaskManagerSystem.Common.CommonMiddlewares
{
    public class TaskCancellationMiddleware(RequestDelegate next, ILogger<TaskCancellationMiddleware> logger)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception _) when (_ is OperationCanceledException or TaskCanceledException)
            {
                logger.LogInformation($"Операция по пути {context.Connection.LocalIpAddress} - {context.Request.Path} была отменена...");
            }
        }
    }
}
