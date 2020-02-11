using Microsoft.EntityFrameworkCore.Migrations;

namespace Invoicing.Core.Database.Migrations
{
    public partial class RelationshipBetweenUserAndContractor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contractor_Users_UserId",
                table: "Contractor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contractor",
                table: "Contractor");

            migrationBuilder.RenameTable(
                name: "Contractor",
                newName: "Contractors");

            migrationBuilder.RenameIndex(
                name: "IX_Contractor_UserId",
                table: "Contractors",
                newName: "IX_Contractors_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "BankAccount",
                table: "Contractors",
                maxLength: 34,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contractors",
                table: "Contractors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contractors_Users_UserId",
                table: "Contractors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contractors_Users_UserId",
                table: "Contractors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contractors",
                table: "Contractors");

            migrationBuilder.RenameTable(
                name: "Contractors",
                newName: "Contractor");

            migrationBuilder.RenameIndex(
                name: "IX_Contractors_UserId",
                table: "Contractor",
                newName: "IX_Contractor_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "BankAccount",
                table: "Contractor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 34,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contractor",
                table: "Contractor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contractor_Users_UserId",
                table: "Contractor",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
