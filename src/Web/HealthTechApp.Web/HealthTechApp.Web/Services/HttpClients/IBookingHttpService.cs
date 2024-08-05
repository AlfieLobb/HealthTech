namespace HealthTechApp.Web.Services.HttpClients;

public interface IBookingHttpService
{
    Task ApproveAppointmentAsync(int appointmentId);
    Task CreateAppointmentAsync(CreateAppointmentRequest data);
    Task DeleteAppointmentAsync(int appointmentId);
    Task<string> GetAnonymousPing();
    Task<Appointment?> GetAppointmentAsync(int appointmentId);
    Task<IEnumerable<AppointmentSummary>> GetAppointmentsAsync();
    Task<string> GetAuthedPing();
    Task UpdateCollectionAsync(UpdateAppointmentRequest data);
}