var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWhen(
    context => context.Request.Query.ContainsKey("username"),
    app =>
    {
        app.Use(async (HttpContext context, RequestDelegate next) =>
        {
            await context.Response.WriteAsync("Hello From Middleware Branch");

            await next(context);
        }
        );
    }
);

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello From Middleware at Main Chain");
});

app.Run();
