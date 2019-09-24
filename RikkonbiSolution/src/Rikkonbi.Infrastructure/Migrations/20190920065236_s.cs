using Microsoft.EntityFrameworkCore.Migrations;

namespace Rikkonbi.Infrastructure.Migrations
{
    public partial class s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Borrows_BorrowId",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "BorrowId",
                table: "Devices",
                newName: "DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_BorrowId",
                table: "Devices",
                newName: "IX_Devices_DeviceId");

            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "Borrows",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_DeviceId",
                table: "Borrows",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrows_Borrows_DeviceId",
                table: "Borrows",
                column: "DeviceId",
                principalTable: "Borrows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Devices_DeviceId",
                table: "Devices",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrows_Borrows_DeviceId",
                table: "Borrows");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Devices_DeviceId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Borrows_DeviceId",
                table: "Borrows");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Borrows");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "Devices",
                newName: "BorrowId");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_DeviceId",
                table: "Devices",
                newName: "IX_Devices_BorrowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Borrows_BorrowId",
                table: "Devices",
                column: "BorrowId",
                principalTable: "Borrows",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
