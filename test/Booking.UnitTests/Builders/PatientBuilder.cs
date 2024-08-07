using Booking.Domain.AggregatesModel.PatientAggregate;

namespace Booking.UnitTests.Builders;

public class PatientBuilder()
{
    public Patient Build()
    {
        // Arrange
        var email = "builder@email.co.uk";
        var name = "builder name";
        var contactNumber = "builder38949461";

        return new Patient(email, name, contactNumber);
    }
}
