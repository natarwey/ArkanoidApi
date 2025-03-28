using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArkanoidApi.Migrations
{
    public partial class AddBoughtSkinsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoughtSkins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SkinId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoughtSkins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoughtSkins_BallSkins_SkinId",
                        column: x => x.SkinId,
                        principalTable: "BallSkins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoughtSkins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoughtSkins_SkinId",
                table: "BoughtSkins",
                column: "SkinId");

            migrationBuilder.CreateIndex(
                name: "IX_BoughtSkins_UserId",
                table: "BoughtSkins",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoughtSkins");
        }
    }
}
