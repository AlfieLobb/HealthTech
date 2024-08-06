using Booking.Domain.AggregatesModel.ApproverAggregate;

namespace Booking.UnitTests.Builders;

public class ApproverBuilder()
{
    public Approver Build()
    {
        var identityGuid = "44e068f1-1505-4ca8-81ff-46cc8e5b8c9a";
        var name = "";

        return new Approver(identityGuid, name);
    }
}