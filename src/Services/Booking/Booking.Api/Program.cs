var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddDefaultOpenApi();
builder.AddApplicationServices();

var app = builder.Build();

app.UseDefaultOpenApi();
app.MapDefaultEndpoints();

app.MapGroup("api/bookings")
    .MapBookingsApi()
    .WithTags("Bookings API")
    .WithOpenApi();

app.Run();
