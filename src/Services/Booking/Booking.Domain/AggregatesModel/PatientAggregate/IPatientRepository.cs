using Booking.Domain.Seedwork;

namespace Booking.Domain.AggregatesModel.PatientAggregate;

public interface IPatientRepository : IRepository<Patient>
{
    Patient Add(Patient patient);
    Patient Update(Patient patient);
    Task<Patient> FindAsync(string patientEmail);
    Task<Patient> FindByIdAsync(string id);
}
