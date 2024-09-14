var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// app.MapGet("/", () => "Hello World!");


// Status Code & Headers & Query
app.Run(async (HttpContext context) =>
{
    string path = context.Request.Path;
    string method = context.Request.Method;
    var Query = context.Request.Query;
    context.Request.Headers["Content-Type"] = "text/html";
    context.Response.Headers["Server-Details"] = "First Project";
    context.Response.StatusCode = 200;
    context.Response.Headers["Content-Type"] = "text/html";
    await context.Response.WriteAsync($"<h1>{method} {path}</h1>");
    if (method == "GET")
    {
        if (Query.ContainsKey("id"))
        {
            string id = Query["id"];
            await context.Response.WriteAsync($"<h3>ID: {id}</h3>");
        }
    }


});

app.Run();
