namespace Booking.Application.Commands;

public record UpdateAppointmentCommand(int AppointmentId, DateTime AppointmentDate, string Issue, string Email, string Name, string ContactNumber) : IRequest<bool>;
