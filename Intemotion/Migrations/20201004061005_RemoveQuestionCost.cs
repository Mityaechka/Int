using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class RemoveQuestionCost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntellectualQuestionCosts_FirstRounds_FirstRoundId",
                table: "IntellectualQuestionCosts");

            migrationBuilder.AddForeignKey(
                name: "FK_IntellectualQuestionCosts_FirstRounds_FirstRoundId",
                table: "IntellectualQuestionCosts",
                column: "FirstRoundId",
                principalTable: "FirstRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntellectualQuestionCosts_FirstRounds_FirstRoundId",
                table: "IntellectualQuestionCosts");

            migrationBuilder.AddForeignKey(
                name: "FK_IntellectualQuestionCosts_FirstRounds_FirstRoundId",
                table: "IntellectualQuestionCosts",
                column: "FirstRoundId",
                principalTable: "FirstRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
