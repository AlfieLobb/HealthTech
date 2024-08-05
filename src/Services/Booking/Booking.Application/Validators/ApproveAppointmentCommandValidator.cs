using Booking.Application.Commands;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Validators;

public class ApproveAppointmentCommandValidator : AbstractValidator<ApproveAppointmentCommand>
{
    public ApproveAppointmentCommandValidator(ILogger<ApproveAppointmentCommandValidator> logger)
    {
        RuleFor(appointment => appointment.Id).NotEmpty().WithMessage("No appointmentId found");

        logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
    }
}
