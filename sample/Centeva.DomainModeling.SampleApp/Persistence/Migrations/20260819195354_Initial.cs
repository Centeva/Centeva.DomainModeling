using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Centeva.DomainModeling.SampleApp.Persistence.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "BankAccounts",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                AccountNumber = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                IsClosed = table.Column<bool>(type: "INTEGER", nullable: false),
                Balance_Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                Balance_Currency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                OwnerName_FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                OwnerName_LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BankAccounts", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_BankAccounts_AccountNumber",
            table: "BankAccounts",
            column: "AccountNumber",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "BankAccounts");
    }
}
