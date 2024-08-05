using Booking.Application.Services.Identity;
using Booking.Domain.AggregatesModel.AppointmentAggregate;
using Booking.Domain.AggregatesModel.ApproverAggregate;
using Booking.Messages;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Commands;

public class ApproveAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository,
    IApproverRepository approverRepository,
    IIdentityService identityService,
    IMediator mediator,
    ILogger<ApproveAppointmentCommandHandler> logger,
    IPublishEndpoint publishEndpoint)
    : IRequestHandler<ApproveAppointmentCommand, bool>
{
    public async Task<bool> Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
    {
        var userIdentity = identityService.GetUserIdentity();
        var approver = await approverRepository.FindAsync(userIdentity);

        var approverExisted = approver is not null;

        if (!approverExisted)
        {
            //TODO: Pull UserName from context
            approver = new Approver(userIdentity, "admin");
            logger.LogInformation("Creating Approver - Approver: {@Approver}", approver);
        }
        var patientUpdate = approverExisted
            ? approverRepository.Update(approver)
            : approverRepository.Add(approver);

        await approverRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);


        var appointment = await appointmentRepository.GetAsync(request.Id);

        if (appointment is null)
        {
            return false;
        }

        appointment.SetApprover(approver.Id);
        appointment.SetApprovalDate(DateTime.UtcNow);

        appointmentRepository.Update(appointment);


        await appointmentRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);

        await publishEndpoint.Publish(new BookingUpdatedIntegrationEvent());
        return true;

    }
}