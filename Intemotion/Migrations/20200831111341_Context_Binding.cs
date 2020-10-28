using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class Context_Binding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntellectualAnswer_IntellectualQuestion_IntellectualQuestionId",
                table: "IntellectualAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_IntellectualQuestion_QuestionsCategorys_QuestionsCategoryId",
                table: "IntellectualQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_IntellectualQuestionCosts_FirstRounds_FirstRoundId",
                table: "IntellectualQuestionCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsCategorys_FirstRounds_FirstRoundId",
                table: "QuestionsCategorys");

            migrationBuilder.AddForeignKey(
                name: "FK_IntellectualAnswer_IntellectualQuestion_IntellectualQuestionId",
                table: "IntellectualAnswer",
                column: "IntellectualQuestionId",
                principalTable: "IntellectualQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IntellectualQuestion_QuestionsCategorys_QuestionsCategoryId",
                table: "IntellectualQuestion",
                column: "QuestionsCategoryId",
                principalTable: "QuestionsCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IntellectualQuestionCosts_FirstRounds_FirstRoundId",
                table: "IntellectualQuestionCosts",
                column: "FirstRoundId",
                principalTable: "FirstRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsCategorys_FirstRounds_FirstRoundId",
                table: "QuestionsCategorys",
                column: "FirstRoundId",
                principalTable: "FirstRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntellectualAnswer_IntellectualQuestion_IntellectualQuestionId",
                table: "IntellectualAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_IntellectualQuestion_QuestionsCategorys_QuestionsCategoryId",
                table: "IntellectualQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_IntellectualQuestionCosts_FirstRounds_FirstRoundId",
                table: "IntellectualQuestionCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsCategorys_FirstRounds_FirstRoundId",
                table: "QuestionsCategorys");

            migrationBuilder.AddForeignKey(
                name: "FK_IntellectualAnswer_IntellectualQuestion_IntellectualQuestionId",
                table: "IntellectualAnswer",
                column: "IntellectualQuestionId",
                principalTable: "IntellectualQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IntellectualQuestion_QuestionsCategorys_QuestionsCategoryId",
                table: "IntellectualQuestion",
                column: "QuestionsCategoryId",
                principalTable: "QuestionsCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IntellectualQuestionCosts_FirstRounds_FirstRoundId",
                table: "IntellectualQuestionCosts",
                column: "FirstRoundId",
                principalTable: "FirstRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsCategorys_FirstRounds_FirstRoundId",
                table: "QuestionsCategorys",
                column: "FirstRoundId",
                principalTable: "FirstRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
