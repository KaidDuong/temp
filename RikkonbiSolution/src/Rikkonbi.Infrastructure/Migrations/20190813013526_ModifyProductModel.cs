using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rikkonbi.Infrastructure.Migrations
{
    public partial class ModifyProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QRImage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "QRText",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "QrCodeContent",
                table: "Products",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QrCodeImageUrl",
                table: "Products",
                maxLength: 300,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QrCodeContent",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "QrCodeImageUrl",
                table: "Products");

            migrationBuilder.AddColumn<byte[]>(
                name: "QRImage",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QRText",
                table: "Products",
                nullable: true);
        }
    }
}
