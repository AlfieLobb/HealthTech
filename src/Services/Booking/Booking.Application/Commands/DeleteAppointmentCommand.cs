namespace Booking.Application.Commands;

public record DeleteAppointmentCommand(int Id) : IRequest<bool>;
