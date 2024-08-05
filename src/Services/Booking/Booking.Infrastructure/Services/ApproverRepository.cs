using Booking.Domain.AggregatesModel.ApproverAggregate;
using Booking.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Services;

public class ApproverRepository : IApproverRepository
{
    private readonly BookingContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public ApproverRepository(BookingContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Approver Add(Approver approver)
    {
        if (approver.IsTransient())
        {
            return _context.Approvers
                .Add(approver)
                .Entity;
        }

        return approver;
    }

    public async Task<Approver> FindAsync(string approverIdentityGuid)
    {
        var approver = await _context.Approvers
            .Where(b => b.IdentityGuid == approverIdentityGuid)
            .SingleOrDefaultAsync();

        return approver;
    }

    public async Task<Approver> FindByIdAsync(string id)
    {
        var approver = await _context.Approvers
            .Where(b => b.Id == int.Parse(id))
            .SingleOrDefaultAsync();

        return approver;
    }

    public Approver Update(Approver approver)
    {
        return _context.Approvers
                .Update(approver)
                .Entity;
    }
}
