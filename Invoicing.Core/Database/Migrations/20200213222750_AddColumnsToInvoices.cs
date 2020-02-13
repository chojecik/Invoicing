using Microsoft.EntityFrameworkCore.Migrations;

namespace Invoicing.Core.Database.Migrations
{
    public partial class AddColumnsToInvoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "GrossValue",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NetValue",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VatAmount",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "VatRate",
                table: "Invoices",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrossValue",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "NetValue",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "VatAmount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "VatRate",
                table: "Invoices");
        }
    }
}
