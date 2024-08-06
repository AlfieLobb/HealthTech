using Booking.Domain.AggregatesModel.AppointmentAggregate;

namespace Booking.UnitTests.Builders;

public class AppointmentBuilder()
{

    public Appointment BuildAppointment()
    {
        // Arrange
        // Appointment
        var patientId = 1;
        var issue = "Some Test Issue";
        var appointmentDate = DateTime.UtcNow.AddHours(-1);

        var appointment = new Appointment(patientId, issue, appointmentDate);
        return appointment;
    }

    public Appointment BuildApprovedAppointment()
    {
        // Arrange
        // Appointment
        var patientId = 1;
        var issue = "Some Test Issue";
        var appointmentDate = DateTime.UtcNow.AddHours(-1);

        // Approver
        var approverId = 1;

        var appointment = new Appointment(patientId, issue, appointmentDate);
        appointment.Approve(approverId);
        return appointment;
    }

}