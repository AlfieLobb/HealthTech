
using Booking.Domain.AggregatesModel.AppointmentAggregate;
using Booking.UnitTests.Builders;
using NUnit.Framework.Legacy;

namespace Booking.UnitTests.Domain;

[TestFixture]
public class AppointmentAggregateTest()
{
    [Test]
    public void Create_Appointment_Success()
    {

        // Arrange
        var patientId = 1;
        var issue = "Test Issue";
        var appointmentDate = DateTime.UtcNow;

        // Act
        var appointment = new Appointment(patientId, issue, appointmentDate);

        // Assert
        ClassicAssert.IsNotNull(appointment);
        ClassicAssert.AreEqual(patientId, appointment.GetPatientId);
        ClassicAssert.AreEqual(issue, appointment.GetIssue);
        ClassicAssert.AreEqual(appointmentDate, appointment.GetAppointmentDate);
        ClassicAssert.IsNull(appointment.GetApproverId);
        ClassicAssert.IsNull(appointment.GetApprovalDate);
    }

    [Test]
    public void Update_Appointment_Date_Success()
    {
        // Arrange
        var appointment = new AppointmentBuilder().BuildAppointment();
        var newAppointmentDate = DateTime.UtcNow.AddDays(1);


        // Act
        appointment.SetAppointmentDate(newAppointmentDate);

        // Assert
        ClassicAssert.AreEqual(newAppointmentDate, appointment.GetAppointmentDate);

    }


    [Test]
    public void Update_Appointment_Issue_Success()
    {
        // Arrange
        var appointment = new AppointmentBuilder().BuildAppointment();
        var newIssue = "An updated issue string";


        // Act
        appointment.SetIssue(newIssue);

        // Assert
        ClassicAssert.AreEqual(newIssue, appointment.GetIssue);

    }

    [Test]
    public void Approve_Appointment_Success()
    {
        // Arrange
        var appointment = new AppointmentBuilder().BuildAppointment();
        var approverId = 1;

        // Act
        appointment.Approve(approverId);

        // Assert
        ClassicAssert.AreEqual(approverId, appointment.GetApproverId);
        ClassicAssert.NotNull(appointment.GetApprovalDate);
    }

    [Test]
    public void ClearApprove_Appointment_Success()
    {
        // Arrange
        var appointment = new AppointmentBuilder().BuildApprovedAppointment();

        // Act
        appointment.ClearApproval();

        // Assert
        ClassicAssert.IsNull(appointment.GetApproverId);
        ClassicAssert.IsNull(appointment.GetApprovalDate);
    }
}
