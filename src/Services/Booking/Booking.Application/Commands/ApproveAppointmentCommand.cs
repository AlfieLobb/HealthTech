using MediatR;

namespace Booking.Application.Commands;

public record ApproveAppointmentCommand(int Id) : IRequest<bool>;
