using MediatR;
using System.Runtime.Serialization;

namespace Booking.Application.Commands;

[DataContract]
public class CreateAppointmentCommand : IRequest<bool>
{
    [DataMember]
    public DateTime AppointmentDate { get; private set; } = DateTime.UtcNow;
    [DataMember]
    public string Issue { get; private set; } = string.Empty;
    [DataMember]
    public string Email { get; private set; } = string.Empty;
    [DataMember]
    public string Name { get; private set; } = string.Empty;
    [DataMember]
    public string ContactNumber { get; private set; } = string.Empty;

    public CreateAppointmentCommand()
    {
    }

    public CreateAppointmentCommand(DateTime appointmentDate, string issue, string email, string name, string contactNumber) : this()
    {
        AppointmentDate = appointmentDate;
        Issue = issue;
        Email = email;
        Name = name;
        ContactNumber = contactNumber;
    }
}
