
namespace Booking.Application.Commands;

public class UpdateAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository,
    ILogger<UpdateAppointmentCommandHandler> logger,
    IPatientRepository patientRepository,
    IPublishEndpoint publishEndpoint)
    : IRequestHandler<UpdateAppointmentCommand, bool>
{
    public async Task<bool> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {

        var appointment = await appointmentRepository.GetAsync(request.AppointmentId);
        if (appointment is null)
        {
            return false;
        }

        var patient = await patientRepository.FindAsync(request.Email);
        var patientExsited = patient is not null;

        if (!patientExsited)
        {
            patient = new Patient(request.Email, request.Name, request.ContactNumber);
        }
        if (patient is null)
        {
            return false;
        }

        patient.SetEmail(request.Email);
        patient.SetName(request.Name);
        patient.SetContactNumber(request.ContactNumber);


        var patientUpdate = patientExsited
            ? patientRepository.Update(patient)
            : patientRepository.Add(patient);

        await patientRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        appointment.SetIssue(request.Issue);
        appointment.SetAppointmentDate(request.AppointmentDate);
        appointment.ClearApproval();

        appointmentRepository.Update(appointment);
        logger.LogInformation("Updating Appointment - Appointment: {@appointment}", appointment);
        await appointmentRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);


        await publishEndpoint.Publish(new BookingUpdatedIntegrationEvent(), cancellationToken);
        return true;

    }
}