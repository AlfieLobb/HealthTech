namespace Booking.Domain.AggregatesModel.ApproverAggregate;

public interface IApproverRepository : IRepository<Approver>
{
    Approver Add(Approver approver);
    Approver Update(Approver approver);
    Task<Approver> FindAsync(string approverIdentityGuid);
    Task<Approver> FindByIdAsync(string id);
}
