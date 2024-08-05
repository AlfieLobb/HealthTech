namespace Booking.Domain.AggregatesModel.ApproverAggregate;

public class Approver
    : Entity, IAggregateRoot
{

    public string IdentityGuid { get; private set; }

    public string Name { get; private set; }
    private Approver()
    {

    }
    public Approver(string identity, string name)
    {
        IdentityGuid = !string.IsNullOrWhiteSpace(identity) ? identity : throw new ArgumentNullException(nameof(identity));
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
    }
}
