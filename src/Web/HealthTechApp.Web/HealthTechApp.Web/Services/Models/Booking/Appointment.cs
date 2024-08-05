namespace HealthTechApp.Web.Services.Models.Booking;

public class Appointment
{
    public int Id { get; set; }
    [Required]
    public DateTime AppointmentDate { get; set; } = DateTime.UtcNow;
    [Required]
    public string Issue { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Name { get; set; }
    [Required, Phone]
    public string ContactNumber { get; set; }
}
