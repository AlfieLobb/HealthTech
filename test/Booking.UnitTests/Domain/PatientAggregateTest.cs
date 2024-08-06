
using Booking.Domain.AggregatesModel.PatientAggregate;
using NUnit.Framework.Legacy;

namespace Booking.UnitTests.Domain;

[TestFixture]
public class PatientAggregateTest
{
    public PatientAggregateTest()
    {
    }


    [Test]
    public void Create_Patient_Failure_Email()
    {
        // Arrange
        var email = "email@email.co.uk";
        var name = "test name";
        var contactNumber = "071238949461";


        // Act
        var patient = new Patient(email, name, contactNumber);

        // Assert
        ClassicAssert.IsNotNull(patient);
        ClassicAssert.AreEqual(email, patient.Email);
        ClassicAssert.AreEqual(name, patient.Name);
        ClassicAssert.AreEqual(contactNumber, patient.ContactNumber);

    }


    [Test]
    public void Create_Patient_Failure()
    {
        // Arrange
        string email = null;
        var name = "test name";
        var contactNumber = "071238949461";


        // Act
        ClassicAssert.Throws<ArgumentNullException>(() => new Patient(email, name, contactNumber), "Email");


    }

}
