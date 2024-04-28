using System;

using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace AgileInfoTask
{
    public class RequestLoggingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var requestPath = context.Request.Path;
            var method = context.Request.Method;
            var timestamp = DateTime.UtcNow;

            // Log request details to console (or any logging provider)
            Console.WriteLine($"[{timestamp}] {method} request to {requestPath}");

            // Call the next middleware in the pipeline
            await next(context);
        }
    }
}

