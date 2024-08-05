using HealthTechApp.Web.Services;
using MassTransit;
using Booking.Messages;
namespace HealthTechApp.Web.Consumer;

public class AppointmentApprovedIntegrationConsumer(
    BookingNotificationService bookingNotificationService,
    ILogger<AppointmentApprovedIntegrationConsumer> logger) : IConsumer<BookingApprovedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<BookingApprovedIntegrationEvent> context)
    {
        logger.LogInformation("Handling integration event");
        await bookingNotificationService.NotifyBookingsStatusChangedAsync();
    }
}
