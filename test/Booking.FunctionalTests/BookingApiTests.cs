using Aspire.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Text.Json;
using System.Text;
using Asp.Versioning.Http;
using Asp.Versioning;
using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Testing;
using Xunit.Sdk;

namespace Booking.FunctionalTests;

public class BookingApiTests
{

    private class AutoAuthorizeStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                builder.UseMiddleware<AutoAuthorizeMiddleware>();
                next(builder);
            };
        }
    }
    //[Fact]
    public async Task GetPingAnonymousWorks()
    {
        // Arrange
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.HealthTechApp_AppHost>();
        await using var app = await appHost.BuildAsync();
        await app.StartAsync();

        // Act
        var httpClient = app.CreateHttpClient("booking-api");
        var response = await httpClient.GetAsync("/api/bookings/pinganonymous");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    //[Fact]
    public async Task GetPingAuthenticatedWorks()
    {
        // Arrange
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.HealthTechApp_AppHost>();
        appHost.Services.AddSingleton<IStartupFilter>(new AutoAuthorizeStartupFilter());
        
        await using var app = await appHost.BuildAsync();
        await app.StartAsync();

        // Act
        var httpClient = app.CreateHttpClient("booking-api");
        var response = await httpClient.GetAsync("/api/bookings/pingauthed");
        var s = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}