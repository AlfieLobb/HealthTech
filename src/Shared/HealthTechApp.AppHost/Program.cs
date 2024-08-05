using Aspire.Hosting.Lifecycle;

var builder = DistributedApplication.CreateBuilder(args);


builder.AddForwardedHeaders();

var launchProfileName = "https";

var sql = builder.AddSqlServer("sql", port: 5434);
var identityDb = sql.AddDatabase("IdentityDB");

var identityApi = builder.AddProject<Projects.Identity_Api>("identity-api", launchProfileName)
    .WithExternalHttpEndpoints()
    .WithReference(identityDb);


builder.AddProject<Projects.Booking_Api>("booking-api");

builder.AddProject<Projects.HealthTechApp_Web>("healthtechapp-web");


builder.Build().Run();

internal static class Extensions
{
    /// <summary>
    /// Adds a hook to set the ASPNETCORE_FORWARDEDHEADERS_ENABLED environment variable to true for all projects in the application.
    /// </summary>
    public static IDistributedApplicationBuilder AddForwardedHeaders(this IDistributedApplicationBuilder builder)
    {
        builder.Services.TryAddLifecycleHook<AddForwardHeadersHook>();
        return builder;
    }

    private class AddForwardHeadersHook : IDistributedApplicationLifecycleHook
    {
        public Task BeforeStartAsync(DistributedApplicationModel appModel, CancellationToken cancellationToken = default)
        {
            foreach (var p in appModel.GetProjectResources())
            {
                p.Annotations.Add(new EnvironmentCallbackAnnotation(context =>
                {
                    context.EnvironmentVariables["ASPNETCORE_FORWARDEDHEADERS_ENABLED"] = "true";
                }));
            }

            return Task.CompletedTask;
        }
    }
}