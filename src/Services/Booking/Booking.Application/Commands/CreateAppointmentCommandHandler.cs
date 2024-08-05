namespace Booking.Application.Commands;
public class CreateAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository,
    ILogger<CreateAppointmentCommandHandler> logger,
    IPatientRepository patientRepository,
    IPublishEndpoint publishEndpoint)
    : IRequestHandler<CreateAppointmentCommand, bool>
{
    public async Task<bool> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {

        var patient = await patientRepository.FindAsync(request.Email);
        var patientExsited = patient is not null;

        if (!patientExsited)
        {
            patient = new Patient(request.Email, request.Name, request.ContactNumber);
        }
        if(patient is null)
        {
            return false;
        }

        if (patientExsited)
        {
            // Update the contact details if the patient exists in case they have been changed
            patient.SetName(request.Name);
            patient.SetContactNumber(request.ContactNumber);

        }
        var patientUpdate = patientExsited
            ? patientRepository.Update(patient)
            : patientRepository.Add(patient);

        await patientRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var appointment = new Domain.AggregatesModel.AppointmentAggregate.Appointment(patient.Id, request.Issue, request.AppointmentDate);

        appointmentRepository.Add(appointment);
        logger.LogInformation("Creating Appointment - Appointment: {@appointment}", appointment);
        await appointmentRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);

        await publishEndpoint.Publish(new BookingCreatedIntegrationEvent(), cancellationToken);

        return true;

    }
}
