
using Booking.Domain.AggregatesModel.ApproverAggregate;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework.Legacy;

namespace Booking.UnitTests.Domain;

[TestFixture]
public class ApprovedAggregateTest()
{

    [Test]
    public void Create_Approver_Success()
    {
        // Arrange
        var identity = Guid.NewGuid().ToString();
        var name = "test name";       


        // Act
        var approver = new Approver(identity, name);

        // Assert
        ClassicAssert.IsNotNull(approver);
        ClassicAssert.AreEqual(identity, approver.IdentityGuid);
        ClassicAssert.AreEqual(name, approver.Name);
    }


    [TestCase(null, "test name", "Identity")]
    [TestCase("", "test name", "Identity")]
    [TestCase("44e068f1-1505-4ca8-81ff-46cc8e5b8c9a", null, "Name")]
    [TestCase("44e068f1-1505-4ca8-81ff-46cc8e5b8c9a", "", "Name")]
    public void Create_Approver_Failure(string? email, string? name, string message)
    {
        // Act
        ClassicAssert.Throws<ArgumentNullException>(() => new Approver(email, name), message);
    }
}