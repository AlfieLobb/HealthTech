using HealthTechApp.Web.Services;
using MassTransit;
using Booking.Messages;
namespace HealthTechApp.Web.Consumer;

public class AppointmentDeletedIntegrationConsumer(
    BookingNotificationService bookingNotificationService,
    ILogger<AppointmentDeletedIntegrationConsumer> logger) : IConsumer<BookingDeletedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<BookingDeletedIntegrationEvent> context)
    {
        logger.LogInformation("Handling integration event");
        await bookingNotificationService.NotifyBookingsStatusChangedAsync();
    }
}
