namespace Booking.Infrastructure.EntityConfigurations;

public class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> patientConfiguration)
    {
        patientConfiguration.ToTable("Patients");

        patientConfiguration.HasKey(b => b.Id);

        patientConfiguration.Ignore(b => b.DomainEvents);

        patientConfiguration.Property(b => b.Name)
            .HasMaxLength(200)
            .IsRequired();

        patientConfiguration.Property(b => b.Email)
            .HasMaxLength(200)
            .IsRequired();

        patientConfiguration.Property(b => b.ContactNumber)
            .HasMaxLength(200)
            .IsRequired();
    }
}
