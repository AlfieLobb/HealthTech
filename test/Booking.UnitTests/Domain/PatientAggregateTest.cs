using Booking.Domain.AggregatesModel.PatientAggregate;
using Booking.UnitTests.Builders;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework.Legacy;

namespace Booking.UnitTests.Domain;

[TestFixture]
public class PatientAggregateTest()
{

    [Test]
    public void Create_Patient_Success()
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

    [Test]
    public void Update_Patient_Name()
    {
        // Arrange
        var patient = new PatientBuilder().Build();
        var newName = "New Name";

        // Act
        patient.SetName(newName);

        // Assert
        ClassicAssert.AreEqual(newName, patient.Name);
    }


    [Test]
    public void Update_Patient_Email()
    {
        // Arrange
        var patient = new PatientBuilder().Build();
        var newEmail = "New Email";

        // Act
        patient.SetEmail(newEmail);

        // Assert
        ClassicAssert.AreEqual(newEmail, patient.Email);
    }

    [Test]
    public void Update_Patient_ContactNumber()
    {
        // Arrange
        var patient = new PatientBuilder().Build();
        var contactNumber = "0798894984984";

        // Act
        patient.SetContactNumber(contactNumber);

        // Assert
        ClassicAssert.AreEqual(contactNumber, patient.ContactNumber);
    }
}
