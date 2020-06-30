using Microsoft.EntityFrameworkCore.Migrations;

namespace PostCardAppV2.Migrations
{
    public partial class remodelpaperforcsv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Papers");

            migrationBuilder.AddColumn<int>(
                name: "PaperCoating",
                table: "Papers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PaperColor",
                table: "Papers",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaperStockType",
                table: "Papers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Weight",
                table: "Papers",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaperCoating",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "PaperColor",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "PaperStockType",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Papers");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Papers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);
        }
    }
}
