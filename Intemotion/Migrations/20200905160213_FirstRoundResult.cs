using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class FirstRoundResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FirstRoundResultId",
                table: "IntellectualQuestion",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirstRoundResultId",
                table: "GameProcesses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FirstRoundResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstRoundResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FirstRoundResulAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    QuestionId = table.Column<int>(nullable: false),
                    AnswerId = table.Column<int>(nullable: false),
                    IsCorrect = table.Column<bool>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    FirstRoundResultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstRoundResulAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FirstRoundResulAnswer_IntellectualAnswer_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "IntellectualAnswer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FirstRoundResulAnswer_FirstRoundResults_FirstRoundResultId",
                        column: x => x.FirstRoundResultId,
                        principalTable: "FirstRoundResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FirstRoundResulAnswer_IntellectualQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "IntellectualQuestion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FirstRoundResulAnswer_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IntellectualQuestion_FirstRoundResultId",
                table: "IntellectualQuestion",
                column: "FirstRoundResultId");

            migrationBuilder.CreateIndex(
                name: "IX_GameProcesses_FirstRoundResultId",
                table: "GameProcesses",
                column: "FirstRoundResultId");

            migrationBuilder.CreateIndex(
                name: "IX_FirstRoundResulAnswer_AnswerId",
                table: "FirstRoundResulAnswer",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_FirstRoundResulAnswer_FirstRoundResultId",
                table: "FirstRoundResulAnswer",
                column: "FirstRoundResultId");

            migrationBuilder.CreateIndex(
                name: "IX_FirstRoundResulAnswer_QuestionId",
                table: "FirstRoundResulAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_FirstRoundResulAnswer_UserId",
                table: "FirstRoundResulAnswer",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameProcesses_FirstRoundResults_FirstRoundResultId",
                table: "GameProcesses",
                column: "FirstRoundResultId",
                principalTable: "FirstRoundResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IntellectualQuestion_FirstRoundResults_FirstRoundResultId",
                table: "IntellectualQuestion",
                column: "FirstRoundResultId",
                principalTable: "FirstRoundResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameProcesses_FirstRoundResults_FirstRoundResultId",
                table: "GameProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_IntellectualQuestion_FirstRoundResults_FirstRoundResultId",
                table: "IntellectualQuestion");

            migrationBuilder.DropTable(
                name: "FirstRoundResulAnswer");

            migrationBuilder.DropTable(
                name: "FirstRoundResults");

            migrationBuilder.DropIndex(
                name: "IX_IntellectualQuestion_FirstRoundResultId",
                table: "IntellectualQuestion");

            migrationBuilder.DropIndex(
                name: "IX_GameProcesses_FirstRoundResultId",
                table: "GameProcesses");

            migrationBuilder.DropColumn(
                name: "FirstRoundResultId",
                table: "IntellectualQuestion");

            migrationBuilder.DropColumn(
                name: "FirstRoundResultId",
                table: "GameProcesses");
        }
    }
}
