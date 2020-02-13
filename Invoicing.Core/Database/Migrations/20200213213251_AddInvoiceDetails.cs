using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invoicing.Core.Database.Migrations
{
    public partial class AddInvoiceDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "GrossAmount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "NetAmount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "VatAmount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "VatRate",
                table: "Invoices");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfIssue",
                table: "Invoices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfService",
                table: "Invoices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(maxLength: 200, nullable: false),
                    PKWiU = table.Column<string>(maxLength: 100, nullable: true),
                    NetPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Unit = table.Column<string>(nullable: true),
                    NetValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatRate = table.Column<int>(nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_InvoiceId",
                table: "InvoiceDetails",
                column: "InvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "DateOfIssue",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DateOfService",
                table: "Invoices");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "GrossAmount",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NetAmount",
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
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
