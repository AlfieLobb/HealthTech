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
