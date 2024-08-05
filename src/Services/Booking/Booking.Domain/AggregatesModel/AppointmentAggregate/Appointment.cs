

namespace Booking.Domain.AggregatesModel.AppointmentAggregate;
public class Appointment
    : Entity, IAggregateRoot
{
    private int _patientId;
    public int GetPatientId => _patientId;
    
    private int? _approverId;
    public int? GetApproverId => _approverId;

    private string _issue = string.Empty;    
    private DateTime _appointmentDate = DateTime.UtcNow;
    private DateTime? _approvalDate;

    protected Appointment()
    {
    }

    public Appointment(int patientId, string issue, DateTime appointmentDate) : this()
    {
        _patientId = patientId;
        _issue = issue;
        _appointmentDate = appointmentDate;
        _approvalDate = null;
        _approverId = null;
    }

    public void SetIssue(string issue) => _issue = issue;
    public void SetAppointmentDate(DateTime appointmentDate) => _appointmentDate = appointmentDate;
    public void SetApprovalDate(DateTime? approvalDate) => _approvalDate = approvalDate;
    public void SetApprover(int? approverId) => _approverId= approverId;
}
