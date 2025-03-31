﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Api.Handlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails
            {
                Instance = httpContext.Request.Path
            };

            if (exception is FluentValidation.ValidationException fluentException)
            {
                problemDetails.Title = "Ошибка валидации";
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                problemDetails.Extensions.Add("message", fluentException.Message);
            }
            else
            {
                problemDetails.Title = "Внутренняя ошибка сервера";
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                problemDetails.Extensions.Add("message", exception.Message);
            }

            problemDetails.Status = httpContext.Response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
            return true;
        }
    }
}
