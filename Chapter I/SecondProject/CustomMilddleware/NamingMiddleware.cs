namespace MiddlewareExample.CustomMiddlware
{
    public class NamingMiddleware
    {
        public readonly RequestDelegate _next;

        public NamingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var query = context.Request.Query;
            if (query.ContainsKey("firstname") && query.ContainsKey("lastname"))
            {
                string fullname = query["firstname"] + " " + query["lastname"];
                await context.Response.WriteAsync("Full Name: " + fullname);
            }
            await _next(context);
        }
    }

    public static class NamingMiddlewareExtensions
    {

        public static IApplicationBuilder UseNamingMiddleware(this IApplicationBuilder app)
        {

            return app.UseMiddleware<NamingMiddleware>();
        }

    }
}