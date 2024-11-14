using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Qel.Ef.Models.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Persons");

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
            new() { Id = 1, FirstName = "Иван", LastName = "Иванов", Birthdate = DateTime.MinValue},
            new() { Id = 2, FirstName = "Владимир", LastName = "Горбатый", Birthdate = DateTime.MaxValue},
            ]);
    }
}