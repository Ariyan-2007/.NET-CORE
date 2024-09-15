var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Enable Routing
app.UseRouting();


//Create Endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.Map("/home", async (context) =>
    {
        await context.Response.WriteAsync("At Home");
    });

    endpoints.Map("/office", async (context) =>
    {
        await context.Response.WriteAsync("At Office");
    });
});

app.Run(async context =>
{
    await context.Response.WriteAsync($"Request Received at {context.Request.Path}");
});

app.Run();
