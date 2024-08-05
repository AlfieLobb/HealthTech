namespace HealthTechApp.Web.Services.Models.Booking;

public record CreateAppointmentRequest(DateTime AppointmentDate, string Issue, string Email, string Name, string ContactNumber);
