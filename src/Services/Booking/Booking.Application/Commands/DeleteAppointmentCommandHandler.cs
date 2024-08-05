namespace Booking.Application.Commands;

public class DeleteAppointmentCommandHandler(ILogger<DeleteAppointmentCommandHandler> logger, IAppointmentRepository appointmentRepository, IPublishEndpoint publishEndpoint)
    : IRequestHandler<DeleteAppointmentCommand, bool>
{
    public async Task<bool> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await appointmentRepository.GetAsync(request.Id);
        if (appointment is null)
        {
            logger.LogInformation("Deleting Appointment - None found");
            return false;
        }

        logger.LogInformation("Deleting Appointment - Appointment: {@Appointment}", appointment);
        appointmentRepository.Delete(appointment);

        await publishEndpoint.Publish(new BookingDeletedIntegrationEvent());

        return await appointmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
