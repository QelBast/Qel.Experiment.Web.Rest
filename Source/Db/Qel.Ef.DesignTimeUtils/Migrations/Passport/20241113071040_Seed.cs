using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qel.Ef.DesignTimeUtils.Migrations.Passport;

/// <inheritdoc />
public partial class Seed : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Passports",
            columns: ["Serie", "Number"],
            values: new object[,]
            {
                {(object)"124121", (object)"124121"},
                {(object)"test", (object)"test"}
            }
        );

        migrationBuilder.InsertData(
            table: "Persons",
            columns: ["FirstName", "LastName", "Birthdate", "PassportId"],
            values: new object[,]
            {
                {(object)"Вася", (object)"Пупкин", DateTime.UtcNow.AddYears(-20), 1},
                {(object)"test", (object)"test", DateTime.UtcNow, 2}
            }
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {

    }
}
