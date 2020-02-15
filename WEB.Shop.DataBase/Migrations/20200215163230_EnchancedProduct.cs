using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB.Shop.DataBase.Migrations
{
    public partial class EnchancedProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Producer",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceUrl",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Producer",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SourceUrl",
                table: "Products");
        }
    }
}
