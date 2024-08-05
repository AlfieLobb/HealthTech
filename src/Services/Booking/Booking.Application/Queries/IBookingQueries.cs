namespace Booking.Application.Queries;
public interface IBookingQueries
{
    Task<ViewModels.Appointment?> GetAppointment(int id);
    Task<IEnumerable<AppointmentSummary>> GetAppointments();
}
