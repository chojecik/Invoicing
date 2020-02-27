using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invoicing.Core.Database.Migrations
{
    public partial class AddDateOfPaymentToInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfPayment",
                table: "Invoices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfPayment",
                table: "Invoices");
        }
    }
}
