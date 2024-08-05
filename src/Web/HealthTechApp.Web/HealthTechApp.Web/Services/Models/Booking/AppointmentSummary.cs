namespace HealthTechApp.Web.Services.Models.Booking;

public record AppointmentSummary(int Id, DateTime AppointmentDate, string Issue, string Email, string Name, string ContactNumber, string? ApproverName, bool Approved);
