using MiddlewareExample.CustomMiddlware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<CustomMiddleware>();
var app = builder.Build();

// Middleware 1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello \n");

    await next(context);
});

//Middleware 2

// app.UseMiddleware<CustomMiddleware>();
// app.UseCustomMiddleware();
app.UseNamingMiddleware();

//Middleware 3
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("World \n");
});

app.Run();
