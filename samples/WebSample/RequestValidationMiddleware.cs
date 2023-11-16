using Microsoft.AspNetCore.Http;
using Serilog;
using System.Threading.Tasks;

namespace WebSample
{
    internal class RequestValidationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var path = context.Request.Path.Value.ToLower();

            Log.Information("Path requested: {path}", path);

            await next(context);
        }
    }
}