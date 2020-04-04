using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB.Shop.DataBase.Migrations
{
    public partial class AdditionalProductFiledsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OnSale",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SaleDeadline",
                table: "Products",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SaleDescription",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SaleValue",
                table: "Products",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnSale",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SaleDeadline",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SaleDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SaleValue",
                table: "Products");
        }
    }
}
