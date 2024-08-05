﻿public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddAuthenticationServices();

        // Application services
        builder.Services.AddScoped<LogOutService>();
        builder.Services.AddSingleton<BookingNotificationService>();

        builder.AddMassTransitAndConsumers();


        builder.Services.AddHttpClient<IBookingHttpService, BookingHttpService>(o => o.BaseAddress = new("https://booking-api"))
            .AddAuthToken();
    }


    public static void AddAuthenticationServices(this IHostApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;

        JsonWebTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

        var identityUrl = configuration.GetRequiredValue("IdentityUrl");
        var callBackUrl = configuration.GetRequiredValue("CallBackUrl");
        var sessionCookieLifetime = configuration.GetValue("SessionCookieLifetimeMinutes", 60);

        // Add Authentication services
        services.AddAuthorization();
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie(options => options.ExpireTimeSpan = TimeSpan.FromMinutes(sessionCookieLifetime))
        .AddOpenIdConnect(options =>
        {
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.Authority = identityUrl;
            options.SignedOutRedirectUri = callBackUrl;
            options.ClientId = "webapp";
            options.ClientSecret = "secret";
            options.ResponseType = "code";
            options.SaveTokens = true;
            options.GetClaimsFromUserInfoEndpoint = true;
            options.RequireHttpsMetadata = false;
            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.Scope.Add("bookings");
        });

        // Blazor auth services
        services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
        services.AddCascadingAuthenticationState();
    }

    private static void AddMassTransitAndConsumers(this IHostApplicationBuilder builder)
    {

        builder.Services.Configure<MassTransitHostOptions>(options =>
        {
            options.WaitUntilStarted = true;
        });
        builder.Services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumer<AppointmentCreatedIntegrationConsumer>();
            busConfigurator.AddConsumer<AppointmentUpdatedIntegrationConsumer>();
            busConfigurator.AddConsumer<AppointmentDeletedIntegrationConsumer>();
            busConfigurator.AddConsumer<AppointmentApprovedIntegrationConsumer>();

            busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
            {
                var configService = context.GetRequiredService<IConfiguration>();
                var connectionString = configService.GetConnectionString("rabbitmq"); // <--- same name as in the orchestration
                busFactoryConfigurator.Host(connectionString);
                busFactoryConfigurator.ConfigureEndpoints(context);
                busFactoryConfigurator.ReceiveEndpoint($"{nameof(AppointmentCreatedIntegrationConsumer)}-WebApi", e =>
                {
                    e.ConfigureConsumer<AppointmentCreatedIntegrationConsumer>(context);
                });
                busFactoryConfigurator.ReceiveEndpoint($"{nameof(AppointmentUpdatedIntegrationConsumer)}-WebApi", e =>
                {
                    e.ConfigureConsumer<AppointmentUpdatedIntegrationConsumer>(context);
                });
                busFactoryConfigurator.ReceiveEndpoint($"{nameof(AppointmentDeletedIntegrationConsumer)}-WebApi", e =>
                {
                    e.ConfigureConsumer<AppointmentDeletedIntegrationConsumer>(context);
                });
                busFactoryConfigurator.ReceiveEndpoint($"{nameof(AppointmentApprovedIntegrationConsumer)}-WebApi", e =>
                {
                    e.ConfigureConsumer<AppointmentApprovedIntegrationConsumer>(context);
                });
            });
        });
    }

    public static async Task<string?> GetUserIdAsync(this AuthenticationStateProvider authenticationStateProvider)
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        return user.FindFirst("sub")?.Value;
    }

    public static async Task<string?> GetUserNameAsync(this AuthenticationStateProvider authenticationStateProvider)
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        return user.FindFirst("name")?.Value;
    }
}

