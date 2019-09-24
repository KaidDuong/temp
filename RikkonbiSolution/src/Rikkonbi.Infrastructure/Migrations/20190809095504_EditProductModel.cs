using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rikkonbi.Infrastructure.Migrations
{
    public partial class EditProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "QRImage",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QRText",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QRImage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "QRText",
                table: "Products");
        }
    }
}
