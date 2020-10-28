using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class Game_ThirdRound : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThirdRoundId",
                table: "Games",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_ThirdRoundId",
                table: "Games",
                column: "ThirdRoundId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_ThirdRounds_ThirdRoundId",
                table: "Games",
                column: "ThirdRoundId",
                principalTable: "ThirdRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_ThirdRounds_ThirdRoundId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_ThirdRoundId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ThirdRoundId",
                table: "Games");
        }
    }
}
