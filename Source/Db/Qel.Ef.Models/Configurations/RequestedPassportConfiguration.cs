using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Qel.Ef.Models.Configurations;

public class RequestedPassportConfiguration : IEntityTypeConfiguration<RequestedPassport>
{
    public void Configure(EntityTypeBuilder<RequestedPassport> builder)
    {
        builder.ToTable("RequestedPassports");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Number)
            .HasMaxLength(16)
            .IsRequired();
        builder.Property(e => e.Serie)
            .HasMaxLength(8)
            .IsRequired();
    }
}