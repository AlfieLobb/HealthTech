﻿namespace Booking.Application.Commands;

public class ApproveAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository,
    IApproverRepository approverRepository,
    IIdentityService identityService,
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
        if (approver is null)
        {
            return false;
        }

        if (approverExisted)
        {
            approverRepository.Update(approver);
        }
        else
        {
            approverRepository.Add(approver);
        }

        await approverRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);


        var appointment = await appointmentRepository.GetAsync(request.Id);

        if (appointment is null)
        {
            return false;
        }

        appointment.Approve(approver.Id, DateTime.UtcNow);

        appointmentRepository.Update(appointment);


        await appointmentRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);

        await publishEndpoint.Publish(new BookingUpdatedIntegrationEvent(), cancellationToken);
        return true;

    }
}