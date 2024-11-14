using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Qel.Ef.Models.Configurations;

public class RequestedPersonConfiguration : IEntityTypeConfiguration<RequestedPerson>
{
    public void Configure(EntityTypeBuilder<RequestedPerson> builder)
    {
        builder.ToTable("RequestedPersons");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.FirstName)
            .HasMaxLength(128)
            .IsRequired();
        builder.Property(e => e.LastName)
            .HasMaxLength(128)
            .IsRequired();
        builder.Property(e => e.Birthdate)
            .IsRequired();
    }
}