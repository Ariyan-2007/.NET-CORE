using RoutingExample.CustomConstraints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months", typeof(MonthsCustomConstraint));
});

builder.Services.AddTransient<IMyService, MyService>();
builder.Services.AddScoped<IAnotherService, AnotherService>();
builder.Services.AddSingleton<ISingletonService, SingletonService>();

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
    endpoints.Map("/", (IMyService myService) => myService.GetGreeting());
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

    endpoints.Map("employee/profile/{EmployeeName:minlength(3):alpha=Ariyan}", async (context) =>
    {

        // Extract filename and extension from route values
        string? employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);

        // Create a response message that includes the filename and extension
        string? responseMessage = $"In Employee Profile - {employeeName}";

        // Write the response
        await context.Response.WriteAsync(responseMessage);
    });

    endpoints.Map("products/details/{id:int:range(1,1000)=1}", async (context) =>
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

    endpoints.Map("daily-digest-report/{reportdate:datetime}", async (context) =>
    {

        DateTime reportDate = Convert.ToDateTime(context.Request.RouteValues["reportdate"]);
        await context.Response.WriteAsync($"In Daily-Digest-Report - {reportDate.ToShortDateString()}");

    });

    endpoints.Map("cities/{cityid:guid}", async (context) =>
    {
        Guid cityId = Guid.Parse(Convert.ToString
        (context.Request.RouteValues["cityid"])!);
        await context.Response.WriteAsync($"City Information - {cityId}");
    });

    endpoints.Map("sales-report/{year:int:min(1900)}/{month:months}", async (context) =>
    {
        int year = Convert.ToInt32(context.Request.RouteValues["year"]);

        string? month = Convert.ToString(context.Request.RouteValues["month"]);

        await context.Response.WriteAsync($"sales report - {year} - {month}");


    });
});

app.Run(async context =>
{
    await context.Response.WriteAsync($"No Route Matched at {context.Request.Path}");
});

app.Run();
