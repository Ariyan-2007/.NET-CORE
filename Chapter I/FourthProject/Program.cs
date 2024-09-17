var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
//Enable Routing
app.UseRouting();


app.Use(async (HttpContext context, RequestDelegate next) =>
{
    Microsoft.AspNetCore.Http.Endpoint? endPoint = context.GetEndpoint();
    await next(context);
});

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

    endpoints.Map("files/{filename}.{extension}", async (context) =>
    {
        // Extract filename and extension from route values
        string? filename = Convert.ToString(context.Request.RouteValues["filename"]);
        string? extension = Convert.ToString(context.Request.RouteValues["extension"]);

        // Create a response message that includes the filename and extension
        string responseMessage = $"Filename: {filename}, Extension: {extension}";

        // Write the response
        await context.Response.WriteAsync(responseMessage);
    });

    endpoints.Map("employee/profile/{EmployeeName}", async (context) =>
    {

        // Extract filename and extension from route values
        string? employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);

        // Create a response message that includes the filename and extension
        string? responseMessage = $"In Employee Profile - {employeeName}";

        // Write the response
        await context.Response.WriteAsync(responseMessage);
    });

    endpoints.Map("products/details/{id=1}", async (context) =>
    {
        try
        {
            int? id = Convert.ToInt32(context.Request.RouteValues["id"]);
            await context.Response.WriteAsync($"Product Details - {id}");

        }
        catch (System.Exception)
        {
            await context.Response.WriteAsync("Please Provide A Proper Product ID");
        }
    });
});

app.Run(async context =>
{
    await context.Response.WriteAsync($"Request Received at {context.Request.Path}");
});

app.Run();
