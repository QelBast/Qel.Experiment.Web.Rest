using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Qel.Ef.Models.Configurations;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.ToTable("Requests");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            builder.Property(e => e.Period)
                .HasMaxLength(128)
                .IsRequired();
            builder.Property(e => e.Summa)
                .HasMaxLength(128)
                .IsRequired();
            builder.Property(e => e.CreationTime)
                .IsRequired(false);
            builder.Property(e => e.ModifyTime)
                .IsRequired(false);

            builder.HasOne(e => e.Passport)
                .WithMany()
                .HasForeignKey(e => e.PassportId)
                .IsRequired();
            builder.HasOne(e => e.Person)
                .WithMany()
                .HasForeignKey(e => e.PersonId)
                .IsRequired();
    }
}