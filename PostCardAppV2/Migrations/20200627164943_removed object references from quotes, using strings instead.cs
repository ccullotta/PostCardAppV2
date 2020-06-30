using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PostCardAppV2.Migrations
{
    public partial class removedobjectreferencesfromquotesusingstringsinstead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_CardSize_CardSizeId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_CardColor_ColorId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Papers_PaperId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_CardSizeId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_ColorId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_PaperId",
                table: "Quotes");

            migrationBuilder.AddColumn<string>(
                name: "CardSize",
                table: "Quotes",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Quotes",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Quotes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Paper",
                table: "Quotes",
                maxLength: 80,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardSize",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Paper",
                table: "Quotes");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_CardSizeId",
                table: "Quotes",
                column: "CardSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_ColorId",
                table: "Quotes",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_PaperId",
                table: "Quotes",
                column: "PaperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_CardSize_CardSizeId",
                table: "Quotes",
                column: "CardSizeId",
                principalTable: "CardSize",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_CardColor_ColorId",
                table: "Quotes",
                column: "ColorId",
                principalTable: "CardColor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Papers_PaperId",
                table: "Quotes",
                column: "PaperId",
                principalTable: "Papers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
