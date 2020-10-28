using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class SponsorBasnners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SponsorBanner",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SponsorBanner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SponsorBanner_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SponsorBannerGame",
                columns: table => new
                {
                    SponsorBannerId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SponsorBannerGame", x => new { x.GameId, x.SponsorBannerId });
                    table.ForeignKey(
                        name: "FK_SponsorBannerGame_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SponsorBannerGame_SponsorBanner_SponsorBannerId",
                        column: x => x.SponsorBannerId,
                        principalTable: "SponsorBanner",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SponsorBanner_FileId",
                table: "SponsorBanner",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_SponsorBannerGame_SponsorBannerId",
                table: "SponsorBannerGame",
                column: "SponsorBannerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SponsorBannerGame");

            migrationBuilder.DropTable(
                name: "SponsorBanner");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
