

namespace Booking.Domain.AggregatesModel.AppointmentAggregate;
public class Appointment
    : Entity, IAggregateRoot
{
    private int _patientId;
    public int GetPatientId => _patientId;
    
    private int? _approverId;
    public int? GetApproverId => _approverId;

    private string _issue = string.Empty;    
    public string GetIssue => _issue;
    private DateTime _appointmentDate = DateTime.UtcNow;
    public DateTime GetAppointmentDate => _appointmentDate;

    private DateTime? _approvalDate;
    public DateTime? GetApprovalDate => _approvalDate;

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

    public void Approve(int? approverId)
    {
        _approverId = approverId;
        _approvalDate = DateTime.UtcNow;
    }

    public void ClearApproval()
    {
        _approverId = null;
        _approvalDate = null;
    }
}
