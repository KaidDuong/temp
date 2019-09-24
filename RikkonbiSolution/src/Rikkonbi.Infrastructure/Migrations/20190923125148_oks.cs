using Microsoft.EntityFrameworkCore.Migrations;

namespace Rikkonbi.Infrastructure.Migrations
{
    public partial class oks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Borrows",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Borrows",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
