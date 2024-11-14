using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qel.Ef.Models.BlacklistContext;

namespace Qel.Ef.Models.Configurations;

public class BlacklistedPassportDataConfiguration : IEntityTypeConfiguration<BlacklistedPassportData>
{
    public void Configure(EntityTypeBuilder<BlacklistedPassportData> builder)
    {
        builder.ToTable("BlacklistedPassportData");

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
            new() { Id = 2, Serie = "2228", Number = "213455"},
            ]);
    }
}