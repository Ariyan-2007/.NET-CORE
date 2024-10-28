
namespace MiddlewareExample.CustomMiddlware
{
    public class CustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("My Middleware - Starting\n");
            await next(context);
            await context.Response.WriteAsync("My Middleware - Ending\n");
        }
    }

    public static class CustomMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
        {

            return app.UseMiddleware<CustomMiddleware>();
        }
    }
}