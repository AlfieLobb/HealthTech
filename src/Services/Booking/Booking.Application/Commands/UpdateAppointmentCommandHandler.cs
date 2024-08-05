
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

        var appointment = await appointmentRepository.GetAsync(request.AppointmentId);
        if (appointment is null)
        {
            return false;
        }
        appointment.SetIssue(request.Issue);
        appointment.SetApprovalDate(request.AppointmentDate);
        appointment.SetApprovalDate(null);
        appointment.SetApprover(null);


        appointmentRepository.Update(appointment);
        logger.LogInformation("Updating Appointment - Appointment: {@appointment}", appointment);
        await appointmentRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);


        await publishEndpoint.Publish(new BookingUpdatedIntegrationEvent(), cancellationToken);
        return true;

    }
}