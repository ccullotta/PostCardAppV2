using Microsoft.EntityFrameworkCore.Migrations;

namespace PostCardAppV2.Migrations
{
    public partial class removeIDsfromquote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardSizeId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "PaperId",
                table: "Quotes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardSizeId",
                table: "Quotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Quotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaperId",
                table: "Quotes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
