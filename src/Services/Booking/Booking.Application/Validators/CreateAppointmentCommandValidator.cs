using Booking.Application.Commands;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Validators;

public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator(ILogger<CreateAppointmentCommandValidator> logger)
    {
        RuleFor(appointment => appointment.AppointmentDate).NotEmpty().WithMessage("No AppointmentDate found");
        RuleFor(appointment => appointment.Issue).NotEmpty().WithMessage("No Issue found");
        RuleFor(appointment => appointment.Email).NotEmpty().EmailAddress().WithMessage("Invalid Email");
        RuleFor(appointment => appointment.ContactNumber).NotEmpty().WithMessage("No ContactNumber found");
        RuleFor(appointment => appointment.Name).NotEmpty().WithMessage("No Name found");

        logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
    }
}
