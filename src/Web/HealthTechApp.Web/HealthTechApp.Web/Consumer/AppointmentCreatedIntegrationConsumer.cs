using HealthTechApp.Web.Services;
using MassTransit;
using Booking.Messages;
namespace HealthTechApp.Web.Consumer;

public class AppointmentCreatedIntegrationConsumer(
    BookingNotificationService bookingNotificationService,
    ILogger<AppointmentCreatedIntegrationConsumer> logger) : IConsumer<BookingCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<BookingCreatedIntegrationEvent> context)
    {
        logger.LogInformation("Handling integration event");
        await bookingNotificationService.NotifyBookingsStatusChangedAsync();
    }
}
