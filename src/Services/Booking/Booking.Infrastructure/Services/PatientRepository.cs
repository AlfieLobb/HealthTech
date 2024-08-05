
namespace Booking.Infrastructure.Services;

public class PatientRepository : IPatientRepository
{
    private readonly BookingContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public PatientRepository(BookingContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    public Patient Add(Patient patient)
    {
        if (patient.IsTransient())
        {
            return _context.Patients
                .Add(patient)
                .Entity;
        }

        return patient;
    }

    public async Task<Patient> FindAsync(string patientEmail)
    {
        var approver = await _context.Patients
            .Where(b => b.Email == patientEmail)
            .SingleOrDefaultAsync();

        return approver;
    }

    public async Task<Patient> FindByIdAsync(string id)
    {
        var patient = await _context.Patients
            .Where(b => b.Id == int.Parse(id))
            .SingleOrDefaultAsync();

        return patient;
    }

    public Patient Update(Patient patient)
    {
        return _context.Patients
                .Update(patient)
                .Entity;
    }

}
