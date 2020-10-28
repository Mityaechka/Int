using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class _00 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SponsorBannerGame_Games_GameId",
                table: "SponsorBannerGame");

            migrationBuilder.DropForeignKey(
                name: "FK_SponsorBannerGame_SponsorBanners_SponsorBannerId",
                table: "SponsorBannerGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SponsorBannerGame",
                table: "SponsorBannerGame");

            migrationBuilder.RenameTable(
                name: "SponsorBannerGame",
                newName: "SponsorBannerGames");

            migrationBuilder.RenameIndex(
                name: "IX_SponsorBannerGame_SponsorBannerId",
                table: "SponsorBannerGames",
                newName: "IX_SponsorBannerGames_SponsorBannerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SponsorBannerGames",
                table: "SponsorBannerGames",
                columns: new[] { "GameId", "SponsorBannerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SponsorBannerGames_Games_GameId",
                table: "SponsorBannerGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SponsorBannerGames_SponsorBanners_SponsorBannerId",
                table: "SponsorBannerGames",
                column: "SponsorBannerId",
                principalTable: "SponsorBanners",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SponsorBannerGames_Games_GameId",
                table: "SponsorBannerGames");

            migrationBuilder.DropForeignKey(
                name: "FK_SponsorBannerGames_SponsorBanners_SponsorBannerId",
                table: "SponsorBannerGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SponsorBannerGames",
                table: "SponsorBannerGames");

            migrationBuilder.RenameTable(
                name: "SponsorBannerGames",
                newName: "SponsorBannerGame");

            migrationBuilder.RenameIndex(
                name: "IX_SponsorBannerGames_SponsorBannerId",
                table: "SponsorBannerGame",
                newName: "IX_SponsorBannerGame_SponsorBannerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SponsorBannerGame",
                table: "SponsorBannerGame",
                columns: new[] { "GameId", "SponsorBannerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SponsorBannerGame_Games_GameId",
                table: "SponsorBannerGame",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SponsorBannerGame_SponsorBanners_SponsorBannerId",
                table: "SponsorBannerGame",
                column: "SponsorBannerId",
                principalTable: "SponsorBanners",
                principalColumn: "Id");
        }
    }
}
