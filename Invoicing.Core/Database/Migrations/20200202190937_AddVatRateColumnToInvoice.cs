using Microsoft.EntityFrameworkCore.Migrations;

namespace Invoicing.Core.Database.Migrations
{
    public partial class AddVatRateColumnToInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VatRate",
                table: "Invoices",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VatRate",
                table: "Invoices");
        }
    }
}
