
using Booking.Domain.AggregatesModel.PatientAggregate;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
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



    [TestCase(null, "test name", "0709123456", "Email")]
    [TestCase("", "test name", "0709123456", "Email")]
    [TestCase("email@email.co.uk", null, "0709123456", "Name")]
    [TestCase("email@email.co.uk", "", "0709123456", "Name")]
    [TestCase("email@email.co.uk", "test name", null, "ContactNumber")]
    [TestCase("email@email.co.uk", "test name", "", "ContactNumber")]
    public void Create_Patient_Failure(string? email, string? name, string? contactNumber, string message)
    {
        // Act
        ClassicAssert.Throws<ArgumentNullException>(() => new Patient(email, name, contactNumber), message);


    }

}
