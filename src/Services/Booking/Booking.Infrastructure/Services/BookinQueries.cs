
using Booking.Application.Queries.ViewModels;

namespace Booking.Infrastructure.Services;
public class BookinQueries(string connectionString) : IBookingQueries
{
    public async Task<Application.Queries.ViewModels.Appointment?> GetAppointment(int id)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var result = await connection.QueryFirstAsync<Application.Queries.ViewModels.Appointment>(@"select 	a.Id, a.AppointmentDate,a.Issue, p.Email, p.Name, p.ContactNumber
FROM Appointments a
LEFT JOIN Patients p on a.PatientId = P.Id
WHERE a.Id = @id", new { id });

        return result ?? null;

    }

    public async Task<IEnumerable<AppointmentSummary>> GetAppointments()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var query = @"SELECT a.Id,
       a.AppointmentDate,
       a.Issue,
       p.Email,
       p.Name,
       p.ContactNumber,
       ap.Name as ApproverName,
       CAST(CASE
         WHEN a.approverid IS NULL THEN 0
         ELSE 1
       END  As BIT)   AS Approved
FROM   appointments a
       LEFT JOIN patients p
              ON a.patientid = p.id
       LEFT JOIN approvers ap
              ON ap.id = a.approverid";
        var result = await connection.QueryAsync<AppointmentSummary>(query);
        return result;
    }
}
