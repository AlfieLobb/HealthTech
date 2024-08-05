namespace HealthTechApp.Web.Services.Models.Booking;

public record UpdateAppointmentRequest(int AppointmentId, DateTime AppointmentDate, string Issue, string Email, string Name, string ContactNumber);
