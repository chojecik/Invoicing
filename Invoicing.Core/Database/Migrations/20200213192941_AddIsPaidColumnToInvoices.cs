using Microsoft.EntityFrameworkCore.Migrations;

namespace Invoicing.Core.Database.Migrations
{
    public partial class AddIsPaidColumnToInvoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Invoices",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Invoices");
        }
    }
}
