using Microsoft.EntityFrameworkCore.Migrations;

namespace PostCardAppV2.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardColor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Multiplier = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardColor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CardSize",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 10, nullable: false),
                    length = table.Column<double>(nullable: false),
                    width = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardSize", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Papers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 40, nullable: true),
                    CompatibleSizes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Papers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sheets",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 10, nullable: false),
                    length = table.Column<double>(nullable: false),
                    width = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sheets", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WithBleed = table.Column<bool>(nullable: false),
                    PaperId = table.Column<int>(nullable: false),
                    CardSizeId = table.Column<int>(nullable: false),
                    ColorId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Quotes_CardSize_CardSizeId",
                        column: x => x.CardSizeId,
                        principalTable: "CardSize",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quotes_CardColor_ColorId",
                        column: x => x.ColorId,
                        principalTable: "CardColor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quotes_Papers_PaperId",
                        column: x => x.PaperId,
                        principalTable: "Papers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaperSheetAssignments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaperId = table.Column<int>(nullable: false),
                    SheetId = table.Column<int>(nullable: false),
                    Cost = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaperSheetAssignments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaperSheetAssignments_Papers_PaperId",
                        column: x => x.PaperId,
                        principalTable: "Papers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaperSheetAssignments_Sheets_SheetId",
                        column: x => x.SheetId,
                        principalTable: "Sheets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaperSheetAssignments_PaperId",
                table: "PaperSheetAssignments",
                column: "PaperId");

            migrationBuilder.CreateIndex(
                name: "IX_PaperSheetAssignments_SheetId",
                table: "PaperSheetAssignments",
                column: "SheetId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaperSheetAssignments");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "Sheets");

            migrationBuilder.DropTable(
                name: "CardSize");

            migrationBuilder.DropTable(
                name: "CardColor");

            migrationBuilder.DropTable(
                name: "Papers");
        }
    }
}
