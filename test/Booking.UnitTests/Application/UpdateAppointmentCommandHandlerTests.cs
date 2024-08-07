
using Booking.Application.Commands;
using Booking.Domain.AggregatesModel.AppointmentAggregate;
using Booking.Domain.AggregatesModel.PatientAggregate;
using Booking.UnitTests.Builders;
using MassTransit;
using MediatR;
using Microsoft.Testing.Platform.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework.Legacy;

namespace Booking.UnitTests.Domain;

[TestFixture]
public class UpdateAppointmentCommandHandlerTests
{


    private IAppointmentRepository _appointmentRepositoryMock;
    private Microsoft.Extensions.Logging.ILogger<UpdateAppointmentCommandHandler> _logger;
    private IPatientRepository _patientRepository;
    private IPublishEndpoint _publishEndpoint;

    [SetUp]
    public void SetupMocks()
    {
        _appointmentRepositoryMock = Substitute.For<IAppointmentRepository>();
        _logger = Substitute.For<Microsoft.Extensions.Logging.ILogger<UpdateAppointmentCommandHandler>>();
        _patientRepository = Substitute.For<IPatientRepository>();
        _publishEndpoint = Substitute.For<IPublishEndpoint>();
    }

    [Test]
    public void UpdateCommand_Run_Success()
    {
        // Arrange
        var updateAppointmentCommandHandler = new UpdateAppointmentCommandHandler(_appointmentRepositoryMock, _logger, _patientRepository, _publishEndpoint);

        var appointmentId = 1;
        var appointmentDate = DateTime.UtcNow;
        var issue = "Issue New";
        var email = "builder@email.co.uk";
        var name = "Some Name";
        var contactNumber = "071231234124";
        var updateCommand = new UpdateAppointmentCommand(appointmentId, appointmentDate, issue, email, name, contactNumber);

        var existingAppointment = new AppointmentBuilder().BuildAppointment();
        _appointmentRepositoryMock.GetAsync(appointmentId).Returns(existingAppointment);

        var existingPatient = new PatientBuilder().Build();
        _patientRepository.FindAsync(existingPatient.Email).Returns(existingPatient);

        // Act
        var response = updateAppointmentCommandHandler.Handle(updateCommand, CancellationToken.None).Result;

        // Assert
        ClassicAssert.IsTrue(response);
        ClassicAssert.AreEqual(appointmentDate, existingAppointment.GetAppointmentDate);
        ClassicAssert.AreEqual(issue, existingAppointment.GetIssue);
        ClassicAssert.IsNull(existingAppointment.GetApproverId);
        ClassicAssert.IsNull(existingAppointment.GetApprovalDate);
        ClassicAssert.AreEqual(email, existingPatient.Email);
        ClassicAssert.AreEqual(contactNumber, existingPatient.ContactNumber);
        ClassicAssert.AreEqual(name, existingPatient.Name);
        _patientRepository.Received(0).Add(Arg.Any<Patient>());
        _patientRepository.Received(1).Update(Arg.Any<Patient>());
    }

    [Test]
    public void UpdateCommand_ApprovedAppointment_Run_Success()
    {
        // Arrange
        var updateAppointmentCommandHandler = new UpdateAppointmentCommandHandler(_appointmentRepositoryMock, _logger, _patientRepository, _publishEndpoint);

        var appointmentId = 1;
        var appointmentDate = DateTime.UtcNow;
        var issue = "Issue New";
        var email = "builder@email.co.uk";
        var name = "Some Name";
        var contactNumber = "071231234124";
        var updateCommand = new UpdateAppointmentCommand(appointmentId, appointmentDate, issue, email, name, contactNumber);

        var existingAppointment = new AppointmentBuilder().BuildApprovedAppointment();
        _appointmentRepositoryMock.GetAsync(appointmentId).Returns(existingAppointment);

        var existingPatient = new PatientBuilder().Build();
        _patientRepository.FindAsync(existingPatient.Email).Returns(existingPatient);

        // Act
        var response = updateAppointmentCommandHandler.Handle(updateCommand, CancellationToken.None).Result;

        // Assert
        ClassicAssert.IsTrue(response);
        ClassicAssert.AreEqual(appointmentDate, existingAppointment.GetAppointmentDate);
        ClassicAssert.AreEqual(issue, existingAppointment.GetIssue);
        ClassicAssert.IsNull(existingAppointment.GetApproverId);
        ClassicAssert.IsNull(existingAppointment.GetApprovalDate);
        ClassicAssert.AreEqual(email, existingPatient.Email);
        ClassicAssert.AreEqual(contactNumber, existingPatient.ContactNumber);
        ClassicAssert.AreEqual(name, existingPatient.Name);
        _patientRepository.Received(0).Add(Arg.Any<Patient>());
        _patientRepository.Received(1).Update(Arg.Any<Patient>());

    }



    [Test]
    public void UpdateCommand_AppointmentIdInvalid_Run_Failure()
    {
        // Arrange
        var updateAppointmentCommandHandler = new UpdateAppointmentCommandHandler(_appointmentRepositoryMock, _logger, _patientRepository, _publishEndpoint);

        var appointmentId = 1;
        var appointmentDate = DateTime.UtcNow;
        var issue = "Issue New";
        var email = "builder@email.co.uk";
        var name = "Some Name";
        var contactNumber = "071231234124";
        var updateCommand = new UpdateAppointmentCommand(appointmentId, appointmentDate, issue, email, name, contactNumber);

        _patientRepository.FindAsync(Arg.Any<string>()).ReturnsNull();

        // Act
        var response = updateAppointmentCommandHandler.Handle(updateCommand, CancellationToken.None).Result;

        // Assert
        ClassicAssert.IsFalse(response);
    }


    [Test]
    public void UpdateCommand_PatientEmailChanged_Run_Success()
    {
        // Arrange
        var updateAppointmentCommandHandler = new UpdateAppointmentCommandHandler(_appointmentRepositoryMock, _logger, _patientRepository, _publishEndpoint);

        var appointmentId = 1;
        var appointmentDate = DateTime.UtcNow;
        var issue = "Issue New";
        var email = "email@email.co.uk";
        var name = "Some Name";
        var contactNumber = "071231234124";
        var updateCommand = new UpdateAppointmentCommand(appointmentId, appointmentDate, issue, email, name, contactNumber);

        var existingAppointment = new AppointmentBuilder().BuildApprovedAppointment();
        _appointmentRepositoryMock.GetAsync(appointmentId).Returns(existingAppointment);
        Patient newPatient = default;
        _patientRepository.FindAsync(Arg.Any<string>()).ReturnsNull();
        _patientRepository.Add(Arg.Any<Patient>()).Returns<Patient>((x) =>
        {
            newPatient = (Patient)x[0];
            return newPatient;
        });

        var response = updateAppointmentCommandHandler.Handle(updateCommand, CancellationToken.None).Result;

        // Assert
        ClassicAssert.IsTrue(response);
        ClassicAssert.AreEqual(appointmentDate, existingAppointment.GetAppointmentDate);
        ClassicAssert.AreEqual(issue, existingAppointment.GetIssue);
        ClassicAssert.IsNull(existingAppointment.GetApproverId);
        ClassicAssert.IsNull(existingAppointment.GetApprovalDate);

        ClassicAssert.IsNotNull(newPatient);
        ClassicAssert.AreEqual(email, newPatient.Email);
        ClassicAssert.AreEqual(contactNumber, newPatient.ContactNumber);
        ClassicAssert.AreEqual(name, newPatient.Name);
        _patientRepository.Received(1).Add(Arg.Any<Patient>());
        _patientRepository.Received(0).Update(Arg.Any<Patient>());

    }
}
