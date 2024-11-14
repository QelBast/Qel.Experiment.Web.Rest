using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Qel.Ef.Models.Configurations;

public class PassportConfiguration : IEntityTypeConfiguration<Passport>
{
    public void Configure(EntityTypeBuilder<Passport> builder)
    {
        builder.ToTable("Passports");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Number)
            .HasMaxLength(16)
            .IsRequired();
        builder.Property(e => e.Serie)
            .HasMaxLength(8)
            .IsRequired();
        
        builder.HasData([
            new() { Id = 1, Serie = "0311", Number = "123456"},
            new() { Id = 2, Serie = "2228", Number = "213455"},
            ]);
    }
}