namespace HealthTechApp.Web.Consumer;

public class AppointmentUpdatedIntegrationConsumer(
    BookingNotificationService bookingNotificationService,
    ILogger<AppointmentUpdatedIntegrationConsumer> logger) : IConsumer<BookingUpdatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<BookingUpdatedIntegrationEvent> context)
    {
        logger.LogInformation("Handling integration event");
        await bookingNotificationService.NotifyBookingsStatusChangedAsync();
    }
}
