using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qel.Ef.Models.BlacklistContext;

namespace Qel.Ef.Models.Configurations;

public class BlacklistedPersonConfiguration : IEntityTypeConfiguration<BlacklistedPerson>
{
    public void Configure(EntityTypeBuilder<BlacklistedPerson> builder)
    {
        builder.ToTable("BlacklistedPersons");

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

        builder.HasOne(e => e.Passport)
            .WithMany()
            .HasForeignKey(e => e.PassportId)
            .IsRequired();        

        builder.HasData([
            new() { Id = 2, FirstName = "Владимир", LastName = "Горбатый", Birthdate = DateTime.MaxValue, PassportId = 2},
            ]);
    }
}