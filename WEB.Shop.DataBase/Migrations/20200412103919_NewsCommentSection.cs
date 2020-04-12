using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB.Shop.DataBase.Migrations
{
    public partial class NewsCommentSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsMainComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    OneNewsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsMainComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsMainComments_News_OneNewsId",
                        column: x => x.OneNewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewsSubComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    NewsMainCommentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsSubComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsSubComments_NewsMainComments_NewsMainCommentId",
                        column: x => x.NewsMainCommentId,
                        principalTable: "NewsMainComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsMainComments_OneNewsId",
                table: "NewsMainComments",
                column: "OneNewsId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsSubComments_NewsMainCommentId",
                table: "NewsSubComments",
                column: "NewsMainCommentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsSubComments");

            migrationBuilder.DropTable(
                name: "NewsMainComments");
        }
    }
}
