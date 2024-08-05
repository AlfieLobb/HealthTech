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

//TODO: Migration Service https://learn.microsoft.com/en-us/dotnet/aspire/database/ef-core-migrations
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BookingContext>();
    context.Database.EnsureCreated();
}
app.Run();
