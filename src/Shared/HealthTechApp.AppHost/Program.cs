using Aspire.Hosting.Lifecycle;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);


builder.AddForwardedHeaders();

var launchProfileName = "https";

var sql = builder.AddSqlServer("sql", port: 5434);
var identityDb = sql.AddDatabase("IdentityDB");

var identityApi = builder.AddProject<Projects.Identity_Api>("identity-api", launchProfileName)
    .WithExternalHttpEndpoints()
    .WithReference(identityDb);
var identityEndpoint = identityApi.GetEndpoint(launchProfileName);


var bookingApi = builder.AddProject<Projects.Booking_Api>("booking-api")
    .WithEnvironment("Identity__Url", identityEndpoint);

var webApp = builder.AddProject<Projects.HealthTechApp_Web>("healthtechapp-web")
    .WithEnvironment("IdentityUrl", identityEndpoint);

webApp.WithEnvironment("CallBackUrl", webApp.GetEndpoint(launchProfileName));

// Identity has a reference to all of the apps for callback urls, this is a cyclic reference
identityApi.WithEnvironment("WebAppClient", webApp.GetEndpoint(launchProfileName))
    .WithEnvironment("BookingApiClient", bookingApi.GetEndpoint(launchProfileName));




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