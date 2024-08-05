using Booking.Application.Behaviours;
using Booking.Application.Commands;
using Booking.Application.Queries;
using Booking.Application.Services.Identity;
using Booking.Application.Validators;
using Booking.Domain.AggregatesModel.AppointmentAggregate;
using Booking.Domain.AggregatesModel.ApproverAggregate;
using Booking.Domain.AggregatesModel.PatientAggregate;
using Booking.Infrastructure;
using Booking.Infrastructure.Behvaiours;
using Booking.Infrastructure.Services;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;

public static partial class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        // Add the authentication services to DI
        builder.AddDefaultAuthentication();

        // Pooling is disabled because of the following error:
        // Unhandled exception. System.InvalidOperationException:
        // The DbContext of type 'BookingContext' cannot be pooled because it does not have a public constructor accepting a single parameter of type DbContextOptions or has more than one constructor.
        builder.Services.AddDbContext<BookingContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("BookingDB"));
        });
        builder.EnrichSqlServerDbContext<BookingContext>();



        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(ApproveAppointmentCommand));

            cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
            cfg.AddOpenBehavior(typeof(ValidatorBehaviour<,>));
            cfg.AddOpenBehavior(typeof(TransactionBehaviour<,>));
        });


        builder.AddMassTransitAndConsumers();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<IIdentityService, IdentityService>();


        builder.Services.AddSingleton<IValidator<ApproveAppointmentCommand>, ApproveAppointmentCommandValidator>();
        builder.Services.AddSingleton<IValidator<CreateAppointmentCommand>, CreateAppointmentCommandValidator>();
        builder.Services.AddSingleton<IValidator<DeleteAppointmentCommand>, DeleteAppointmentCommandValidator>();
        builder.Services.AddSingleton<IValidator<UpdateAppointmentCommand>, UpdateAppointmentDetailsCommandValidator>();

        builder.Services.AddScoped<IBookingQueries>(sp => new BookingQueries(builder.Configuration.GetConnectionString("BookingDB")));
        builder.Services.AddScoped<IApproverRepository, ApproverRepository>();
        builder.Services.AddScoped<IPatientRepository, PatientRepository>();
        builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
    }


    private static void AddMassTransitAndConsumers(this IHostApplicationBuilder builder)
    {

        builder.Services.Configure<MassTransitHostOptions>(options =>
        {
            options.WaitUntilStarted = true;
        });
        builder.Services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
            {
                var configService = context.GetRequiredService<IConfiguration>();
                var connectionString = configService.GetConnectionString("rabbitmq"); // <--- same name as in the orchestration
                busFactoryConfigurator.Host(connectionString);
                busFactoryConfigurator.ConfigureEndpoints(context);
            });
        });
    }
}