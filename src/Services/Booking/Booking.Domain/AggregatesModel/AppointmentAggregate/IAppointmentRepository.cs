using Booking.Domain.Seedwork;

namespace Booking.Domain.AggregatesModel.AppointmentAggregate;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Appointment Add(Appointment appointment);
    void Delete(Appointment appointment);
    Task<Appointment> GetAsync(int appointmentId);
    void Update(Appointment appointment);
}
