using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class Banners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SponsorBanner_Files_FileId",
                table: "SponsorBanner");

            migrationBuilder.DropForeignKey(
                name: "FK_SponsorBannerGame_SponsorBanner_SponsorBannerId",
                table: "SponsorBannerGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SponsorBanner",
                table: "SponsorBanner");

            migrationBuilder.RenameTable(
                name: "SponsorBanner",
                newName: "SponsorBanners");

            migrationBuilder.RenameIndex(
                name: "IX_SponsorBanner_FileId",
                table: "SponsorBanners",
                newName: "IX_SponsorBanners_FileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SponsorBanners",
                table: "SponsorBanners",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SponsorBannerGame_SponsorBanners_SponsorBannerId",
                table: "SponsorBannerGame",
                column: "SponsorBannerId",
                principalTable: "SponsorBanners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SponsorBanners_Files_FileId",
                table: "SponsorBanners",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SponsorBannerGame_SponsorBanners_SponsorBannerId",
                table: "SponsorBannerGame");

            migrationBuilder.DropForeignKey(
                name: "FK_SponsorBanners_Files_FileId",
                table: "SponsorBanners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SponsorBanners",
                table: "SponsorBanners");

            migrationBuilder.RenameTable(
                name: "SponsorBanners",
                newName: "SponsorBanner");

            migrationBuilder.RenameIndex(
                name: "IX_SponsorBanners_FileId",
                table: "SponsorBanner",
                newName: "IX_SponsorBanner_FileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SponsorBanner",
                table: "SponsorBanner",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SponsorBanner_Files_FileId",
                table: "SponsorBanner",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SponsorBannerGame_SponsorBanner_SponsorBannerId",
                table: "SponsorBannerGame",
                column: "SponsorBannerId",
                principalTable: "SponsorBanner",
                principalColumn: "Id");
        }
    }
}
