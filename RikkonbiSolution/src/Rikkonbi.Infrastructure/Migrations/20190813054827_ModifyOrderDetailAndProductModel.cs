using Microsoft.EntityFrameworkCore.Migrations;

namespace Rikkonbi.Infrastructure.Migrations
{
    public partial class ModifyOrderDetailAndProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxOrderQuantity",
                table: "Products",
                nullable: false,
                defaultValue: 10);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OrderDetails",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxOrderQuantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OrderDetails");
        }
    }
}
