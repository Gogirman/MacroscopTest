using CoreStandart.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace CoreStandart.Middleware
{
    /// <summary>
    ///   Компонент Middleware для отслеживания переполнения количества запросов.
    /// </summary>
    public sealed class ThreadMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRequestCounter _counter;
        private IConfiguration _config;

        public ThreadMiddleware(RequestDelegate next, IRequestCounter counter, IConfiguration config)
        {
            _next = next;
            _counter = counter;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (_counter.GetCounter() >= int.Parse(_config["MaxRequestsCount"]))
            {
                throw new OverloadRequestException();
            }

            _counter.Increase();
            await _next(context);
            _counter.Decrease();

        }

    }
}
