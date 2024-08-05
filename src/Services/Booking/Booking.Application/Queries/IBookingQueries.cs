using Booking.Application.Queries.ViewModels;

namespace Booking.Application.Queries;
public interface IBookingQueries
{
    Task<Appointment?> GetAppointment(int id);
    Task<IEnumerable<AppointmentSummary>> GetAppointments();
}
