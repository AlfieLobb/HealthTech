namespace Booking.Domain.AggregatesModel.PatientAggregate;

public class Patient
    : Entity, IAggregateRoot
{

    public string Email { get; private set; }

    public string Name { get; private set; }
    public string ContactNumber { get; private set; }
    private Patient()
    {

    }
    public Patient(string email, string name, string contactNumber)
    {
        Email = !string.IsNullOrWhiteSpace(email) ? email : throw new ArgumentNullException(nameof(email));
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
        ContactNumber = !string.IsNullOrWhiteSpace(contactNumber) ? contactNumber : throw new ArgumentNullException(nameof(contactNumber));
    }

    public void SetEmail(string email) => Email = email;
    public void SetName(string name) => Name = name;
    public void SetContactNumber(string contactNumber) => ContactNumber = contactNumber;
}
