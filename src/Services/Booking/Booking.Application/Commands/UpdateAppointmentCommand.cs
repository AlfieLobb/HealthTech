using Booking.Domain.AggregatesModel.AppointmentAggregate;
using Booking.Domain.AggregatesModel.PatientAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Commands;

public record UpdateAppointmentCommand(int AppointmentId, DateTime AppointmentDate, string Issue, string Email, string Name, string ContactNumber) : IRequest<bool>;
