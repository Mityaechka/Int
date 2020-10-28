using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class Context_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_FirstRound_FirstRoundId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_IntellectualQuestion_QuestionsCategory_QuestionsCategoryId",
                table: "IntellectualQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_IntellectualQuestionCost_FirstRound_FirstRoundId",
                table: "IntellectualQuestionCost");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsCategory_FirstRound_FirstRoundId",
                table: "QuestionsCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionsCategory",
                table: "QuestionsCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IntellectualQuestionCost",
                table: "IntellectualQuestionCost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FirstRound",
                table: "FirstRound");

            migrationBuilder.RenameTable(
                name: "QuestionsCategory",
                newName: "QuestionsCategorys");

            migrationBuilder.RenameTable(
                name: "IntellectualQuestionCost",
                newName: "IntellectualQuestionCosts");

            migrationBuilder.RenameTable(
                name: "FirstRound",
                newName: "FirstRounds");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionsCategory_FirstRoundId",
                table: "QuestionsCategorys",
                newName: "IX_QuestionsCategorys_FirstRoundId");

            migrationBuilder.RenameIndex(
                name: "IX_IntellectualQuestionCost_FirstRoundId",
                table: "IntellectualQuestionCosts",
                newName: "IX_IntellectualQuestionCosts_FirstRoundId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionsCategorys",
                table: "QuestionsCategorys",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IntellectualQuestionCosts",
                table: "IntellectualQuestionCosts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FirstRounds",
                table: "FirstRounds",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_FirstRounds_FirstRoundId",
                table: "Games",
                column: "FirstRoundId",
                principalTable: "FirstRounds",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_FirstRounds_FirstRoundId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_IntellectualQuestion_QuestionsCategorys_QuestionsCategoryId",
                table: "IntellectualQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_IntellectualQuestionCosts_FirstRounds_FirstRoundId",
                table: "IntellectualQuestionCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsCategorys_FirstRounds_FirstRoundId",
                table: "QuestionsCategorys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionsCategorys",
                table: "QuestionsCategorys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IntellectualQuestionCosts",
                table: "IntellectualQuestionCosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FirstRounds",
                table: "FirstRounds");

            migrationBuilder.RenameTable(
                name: "QuestionsCategorys",
                newName: "QuestionsCategory");

            migrationBuilder.RenameTable(
                name: "IntellectualQuestionCosts",
                newName: "IntellectualQuestionCost");

            migrationBuilder.RenameTable(
                name: "FirstRounds",
                newName: "FirstRound");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionsCategorys_FirstRoundId",
                table: "QuestionsCategory",
                newName: "IX_QuestionsCategory_FirstRoundId");

            migrationBuilder.RenameIndex(
                name: "IX_IntellectualQuestionCosts_FirstRoundId",
                table: "IntellectualQuestionCost",
                newName: "IX_IntellectualQuestionCost_FirstRoundId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionsCategory",
                table: "QuestionsCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IntellectualQuestionCost",
                table: "IntellectualQuestionCost",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FirstRound",
                table: "FirstRound",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_FirstRound_FirstRoundId",
                table: "Games",
                column: "FirstRoundId",
                principalTable: "FirstRound",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IntellectualQuestion_QuestionsCategory_QuestionsCategoryId",
                table: "IntellectualQuestion",
                column: "QuestionsCategoryId",
                principalTable: "QuestionsCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IntellectualQuestionCost_FirstRound_FirstRoundId",
                table: "IntellectualQuestionCost",
                column: "FirstRoundId",
                principalTable: "FirstRound",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsCategory_FirstRound_FirstRoundId",
                table: "QuestionsCategory",
                column: "FirstRoundId",
                principalTable: "FirstRound",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
