
using Booking.Application.Commands;
using Booking.Domain.AggregatesModel.AppointmentAggregate;
using Booking.Domain.AggregatesModel.PatientAggregate;
using Booking.UnitTests.Builders;
using MassTransit;
using MediatR;
using Microsoft.Testing.Platform.Logging;
using NSubstitute;
using NUnit.Framework.Legacy;

namespace Booking.UnitTests.Domain;

[TestFixture]
public class UpdateAppointmentCommandHandlerTests
{


    private readonly IAppointmentRepository _appointmentRepositoryMock;
    private readonly Microsoft.Extensions.Logging.ILogger<UpdateAppointmentCommandHandler> _logger;
    private readonly IPatientRepository _patientRepository;
    private IPublishEndpoint _publishEndpoint;
    public UpdateAppointmentCommandHandlerTests()
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
        var email = "email@email.co.uk";
        var name = "Some Name";
        var contactNumber = "071231234124";
        var updateCommand = new UpdateAppointmentCommand(appointmentId, appointmentDate, issue, email, name, contactNumber);

        var existingAppointment = new AppointmentBuilder().BuildAppointment();
        _appointmentRepositoryMock.GetAsync(appointmentId).Returns(existingAppointment);

        // Act
        var response = updateAppointmentCommandHandler.Handle(updateCommand, CancellationToken.None).Result;

        // Assert
        ClassicAssert.IsTrue(response);
        ClassicAssert.AreEqual(appointmentDate, existingAppointment.GetAppointmentDate);
        ClassicAssert.AreEqual(issue, existingAppointment.GetIssue);
        ClassicAssert.IsNull(existingAppointment.GetApproverId);
        ClassicAssert.IsNull(existingAppointment.GetApprovalDate);
        //TODO Test Patient details update
    }

}
