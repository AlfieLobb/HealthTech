namespace Booking.Application.Queries.ViewModels;

public record Appointment(int Id, DateTime AppointmentDate, string Issue, string Email, string Name, string ContactNumber);
