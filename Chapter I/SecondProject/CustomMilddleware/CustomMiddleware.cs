
namespace MiddlewareExample.CustomMiddlware
{
    public class CustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("My Middleware - Starting");
            await next(context);
            await context.Response.WriteAsync("My Middleware - Ending");
        }
    }
}