using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class ThirdRound_Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssociationVariant_Association_AssociationId",
                table: "AssociationVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_ChronologyItem_Chronology_ChronologyId",
                table: "ChronologyItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MelodyGuessVariant_MelodyGuess_MelodyGuessId",
                table: "MelodyGuessVariant");

            migrationBuilder.AddForeignKey(
                name: "FK_AssociationVariant_Association_AssociationId",
                table: "AssociationVariant",
                column: "AssociationId",
                principalTable: "Association",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChronologyItem_Chronology_ChronologyId",
                table: "ChronologyItem",
                column: "ChronologyId",
                principalTable: "Chronology",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MelodyGuessVariant_MelodyGuess_MelodyGuessId",
                table: "MelodyGuessVariant",
                column: "MelodyGuessId",
                principalTable: "MelodyGuess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssociationVariant_Association_AssociationId",
                table: "AssociationVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_ChronologyItem_Chronology_ChronologyId",
                table: "ChronologyItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MelodyGuessVariant_MelodyGuess_MelodyGuessId",
                table: "MelodyGuessVariant");

            migrationBuilder.AddForeignKey(
                name: "FK_AssociationVariant_Association_AssociationId",
                table: "AssociationVariant",
                column: "AssociationId",
                principalTable: "Association",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChronologyItem_Chronology_ChronologyId",
                table: "ChronologyItem",
                column: "ChronologyId",
                principalTable: "Chronology",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MelodyGuessVariant_MelodyGuess_MelodyGuessId",
                table: "MelodyGuessVariant",
                column: "MelodyGuessId",
                principalTable: "MelodyGuess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
