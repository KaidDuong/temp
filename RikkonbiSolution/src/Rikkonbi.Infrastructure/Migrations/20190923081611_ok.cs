using Microsoft.EntityFrameworkCore.Migrations;

namespace Rikkonbi.Infrastructure.Migrations
{
    public partial class ok : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrows_Borrows_DeviceId",
                table: "Borrows");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Devices_DeviceId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_DeviceId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Devices");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrows_Devices_DeviceId",
                table: "Borrows",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrows_Devices_DeviceId",
                table: "Borrows");

            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "Devices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceId",
                table: "Devices",
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
    }
}
