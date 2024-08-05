namespace Booking.Infrastructure.EntityConfigurations;
public class AppointmentEntityTypeConfiguration : IEntityTypeConfiguration<Domain.AggregatesModel.AppointmentAggregate.Appointment>
{
    public void Configure(EntityTypeBuilder<Domain.AggregatesModel.AppointmentAggregate.Appointment> appointmentConfiguration)
    {
        appointmentConfiguration.ToTable("Appointments");

        appointmentConfiguration.HasKey(o => o.Id);

        appointmentConfiguration.Ignore(b => b.DomainEvents);

        appointmentConfiguration
            .Property<int>("_patientId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("PatientId")
            .IsRequired();


        appointmentConfiguration
            .Property<int?>("_approverId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("ApproverId")
            .IsRequired(false);

        appointmentConfiguration
            .Property<DateTime>("_appointmentDate")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("AppointmentDate")
            .IsRequired();

        appointmentConfiguration.Property<string>("_issue")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Issue")
                .IsRequired();

        appointmentConfiguration
            .Property<DateTime?>("_approvalDate")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("ApprovalDate")
            .IsRequired(false);



        appointmentConfiguration.HasOne<Patient>()
            .WithMany()
            .IsRequired(true)
            .HasForeignKey("_patientId");

        appointmentConfiguration.HasOne<Approver>()
            .WithMany()
            .IsRequired(false)
            .HasForeignKey("_approverId");
    }
}
