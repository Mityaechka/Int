using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class ThirdRoundId_null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Association_ThirdRounds_ThirdRoundId",
                table: "Association");

            migrationBuilder.DropForeignKey(
                name: "FK_Chronology_ThirdRounds_ThirdRoundId",
                table: "Chronology");

            migrationBuilder.DropForeignKey(
                name: "FK_MelodyGuess_ThirdRounds_ThirdRoundId",
                table: "MelodyGuess");

            migrationBuilder.AddForeignKey(
                name: "FK_Association_ThirdRounds_ThirdRoundId",
                table: "Association",
                column: "ThirdRoundId",
                principalTable: "ThirdRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chronology_ThirdRounds_ThirdRoundId",
                table: "Chronology",
                column: "ThirdRoundId",
                principalTable: "ThirdRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MelodyGuess_ThirdRounds_ThirdRoundId",
                table: "MelodyGuess",
                column: "ThirdRoundId",
                principalTable: "ThirdRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Association_ThirdRounds_ThirdRoundId",
                table: "Association");

            migrationBuilder.DropForeignKey(
                name: "FK_Chronology_ThirdRounds_ThirdRoundId",
                table: "Chronology");

            migrationBuilder.DropForeignKey(
                name: "FK_MelodyGuess_ThirdRounds_ThirdRoundId",
                table: "MelodyGuess");

            migrationBuilder.AddForeignKey(
                name: "FK_Association_ThirdRounds_ThirdRoundId",
                table: "Association",
                column: "ThirdRoundId",
                principalTable: "ThirdRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chronology_ThirdRounds_ThirdRoundId",
                table: "Chronology",
                column: "ThirdRoundId",
                principalTable: "ThirdRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MelodyGuess_ThirdRounds_ThirdRoundId",
                table: "MelodyGuess",
                column: "ThirdRoundId",
                principalTable: "ThirdRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
