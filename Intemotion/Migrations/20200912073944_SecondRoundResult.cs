using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class SecondRoundResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SecondRoundResultId",
                table: "TruthQuestions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondRoundResultId",
                table: "GameProcesses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondRoundResultId",
                table: "FirstRoundResulAnswer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SecondRoundResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionIndex = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondRoundResults", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TruthQuestions_SecondRoundResultId",
                table: "TruthQuestions",
                column: "SecondRoundResultId");

            migrationBuilder.CreateIndex(
                name: "IX_GameProcesses_SecondRoundResultId",
                table: "GameProcesses",
                column: "SecondRoundResultId");

            migrationBuilder.CreateIndex(
                name: "IX_FirstRoundResulAnswer_SecondRoundResultId",
                table: "FirstRoundResulAnswer",
                column: "SecondRoundResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_FirstRoundResulAnswer_SecondRoundResults_SecondRoundResultId",
                table: "FirstRoundResulAnswer",
                column: "SecondRoundResultId",
                principalTable: "SecondRoundResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameProcesses_SecondRoundResults_SecondRoundResultId",
                table: "GameProcesses",
                column: "SecondRoundResultId",
                principalTable: "SecondRoundResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TruthQuestions_SecondRoundResults_SecondRoundResultId",
                table: "TruthQuestions",
                column: "SecondRoundResultId",
                principalTable: "SecondRoundResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FirstRoundResulAnswer_SecondRoundResults_SecondRoundResultId",
                table: "FirstRoundResulAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_GameProcesses_SecondRoundResults_SecondRoundResultId",
                table: "GameProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_TruthQuestions_SecondRoundResults_SecondRoundResultId",
                table: "TruthQuestions");

            migrationBuilder.DropTable(
                name: "SecondRoundResults");

            migrationBuilder.DropIndex(
                name: "IX_TruthQuestions_SecondRoundResultId",
                table: "TruthQuestions");

            migrationBuilder.DropIndex(
                name: "IX_GameProcesses_SecondRoundResultId",
                table: "GameProcesses");

            migrationBuilder.DropIndex(
                name: "IX_FirstRoundResulAnswer_SecondRoundResultId",
                table: "FirstRoundResulAnswer");

            migrationBuilder.DropColumn(
                name: "SecondRoundResultId",
                table: "TruthQuestions");

            migrationBuilder.DropColumn(
                name: "SecondRoundResultId",
                table: "GameProcesses");

            migrationBuilder.DropColumn(
                name: "SecondRoundResultId",
                table: "FirstRoundResulAnswer");
        }
    }
}
