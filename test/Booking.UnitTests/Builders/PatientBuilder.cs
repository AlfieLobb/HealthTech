using Booking.Domain.AggregatesModel.ApproverAggregate;
using Booking.Domain.AggregatesModel.PatientAggregate;

namespace Booking.UnitTests.Builders;

public class PatientBuilder()
{
    public Patient Build()
    {
        // Arrange
        var email = "email@email.co.uk";
        var name = "test name";
        var contactNumber = "071238949461";

        return new Patient(email, name, contactNumber);
    }
}

public class ApproverBuilder()
{
    public Approver Build()
    {
        var identityGuid = "44e068f1-1505-4ca8-81ff-46cc8e5b8c9a";
        var name = "";

        return new Approver(identityGuid, name);
    }
}