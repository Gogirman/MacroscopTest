using CoreStandart.Exceptions;
using CoreStandart.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreStandart.Middleware
{
    /// <summary>
    ///   Компонент Middleware для обработки ошибок/исключений.
    /// </summary>
    public sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly Dictionary<Type, string> exceptionMessages = new Dictionary<Type, string>
        {
            {typeof(OverloadRequestException), "Count of requests is overloaded" }
        };


        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            var message = ex.Message;
            exceptionMessages.TryGetValue(ex.GetType(), out message);

            var errorJson = JsonSerializer.Serialize(new ExceptionModel
            {
                Message = message,
                ExceptionType = ex.GetType().Name
            });

            _logger.LogError(errorJson);

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(errorJson);
        } 
    }
}
