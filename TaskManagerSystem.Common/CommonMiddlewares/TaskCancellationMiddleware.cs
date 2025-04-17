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
                var request = context.Request;
                var fullUrl = $"{request.Method} {request.Scheme}://{request.Host}{request.Path}{request.QueryString}";

                logger.LogInformation("Запрос был отменён: {FullUrl}", fullUrl);
            }
        }
    }
}
