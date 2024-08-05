using Booking.Application.Commands;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Validators;

public class DeleteAppointmentCommandValidator : AbstractValidator<DeleteAppointmentCommand>
{
    public DeleteAppointmentCommandValidator(ILogger<DeleteAppointmentCommandValidator> logger)
    {
        RuleFor(appointment => appointment.Id).NotEmpty().WithMessage("No appointmentId found");

        logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
    }
}
