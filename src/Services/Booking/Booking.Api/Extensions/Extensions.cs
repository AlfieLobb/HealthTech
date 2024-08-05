

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



// Configure the HTTP request pipeline.



public static partial class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        // Add the authentication services to DI
        builder.AddDefaultAuthentication();
    }
}