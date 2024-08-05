namespace Booking.Infrastructure.EntityConfigurations;

public class ApproverEntityTypeConfiguration : IEntityTypeConfiguration<Approver>
{
    public void Configure(EntityTypeBuilder<Approver> approverConfiguration)
    {
        approverConfiguration.ToTable("Approvers");

        approverConfiguration.HasKey(b => b.Id);

        approverConfiguration.Ignore(b => b.DomainEvents);

        approverConfiguration.HasIndex("IdentityGuid")
            .IsUnique(true);

        approverConfiguration.Property(b => b.Name)
            .HasMaxLength(200)
            .IsRequired();

    }
}
