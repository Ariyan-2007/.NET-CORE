using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// app.MapGet("/", () => "Hello World!");


// Status Code & Headers & Query
app.Use(async (HttpContext context, RequestDelegate next) =>
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

    await next(context);
});


// HTTP GET | POST
app.Run(async (HttpContext context) =>
{

    StreamReader reader = new(context.Request.Body);
    string body = await reader.ReadToEndAsync();
    Dictionary<string, StringValues> queryDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

    if (queryDict.ContainsKey("firstName"))
    {
        string firstName = queryDict["firstName"][0];
        await context.Response.WriteAsync(firstName);
    }

});

app.Run();
