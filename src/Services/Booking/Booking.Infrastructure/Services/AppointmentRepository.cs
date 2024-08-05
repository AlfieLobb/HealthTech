using Booking.Domain.AggregatesModel.AppointmentAggregate;
using Booking.Domain.AggregatesModel.ApproverAggregate;
using Booking.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Services;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly BookingContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public AppointmentRepository(BookingContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public Appointment Add(Appointment appointment)
    {
        if (appointment.IsTransient())
        {
            return _context.Appointments
            .Add(appointment)
                .Entity;
        }

        return appointment;
    }

    public void Delete(Appointment appointment)
    {
        _context.Remove(appointment).State = EntityState.Deleted;
    }

    public async Task<Appointment> GetAsync(int appointmentId)
    {
        var appointment = await _context
                            .Appointments
                            .FirstOrDefaultAsync(o => o.Id == appointmentId);
        return appointment;
    }

    public void Update(Appointment appointment)
    {
        _context.Entry(appointment).State = EntityState.Modified;
    }
}
