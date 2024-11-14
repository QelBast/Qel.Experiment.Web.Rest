using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qel.Ef.DesignTimeUtils.Migrations.Blacklist;

/// <inheritdoc />
public partial class Seed : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "BlacklistedPassportData",
            columns: ["Serie", "Number"],
            values: new object[,]
            {
                {(object)"test", (object)"test"}
            }
        );

        migrationBuilder.InsertData(
            table: "BlacklistedPeson",
            columns: ["FirstName", "LastName", "Birthdate", "PassportId"],
            values: new object[,]
            {
                {(object)"test", (object)"test", DateTime.UtcNow, 1}
            }
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {

    }
}
