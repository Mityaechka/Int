using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class SecondRoundResulAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FirstRoundResulAnswer_SecondRoundResults_SecondRoundResultId",
                table: "FirstRoundResulAnswer");

            migrationBuilder.DropIndex(
                name: "IX_FirstRoundResulAnswer_SecondRoundResultId",
                table: "FirstRoundResulAnswer");

            migrationBuilder.DropColumn(
                name: "SecondRoundResultId",
                table: "FirstRoundResulAnswer");

            migrationBuilder.CreateTable(
                name: "SecondRoundResulAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    QuestionId = table.Column<int>(nullable: false),
                    IsCorrect = table.Column<bool>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    SecondRoundResultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondRoundResulAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecondRoundResulAnswer_TruthQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "TruthQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecondRoundResulAnswer_SecondRoundResults_SecondRoundResultId",
                        column: x => x.SecondRoundResultId,
                        principalTable: "SecondRoundResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecondRoundResulAnswer_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SecondRoundResulAnswer_QuestionId",
                table: "SecondRoundResulAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondRoundResulAnswer_SecondRoundResultId",
                table: "SecondRoundResulAnswer",
                column: "SecondRoundResultId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondRoundResulAnswer_UserId",
                table: "SecondRoundResulAnswer",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecondRoundResulAnswer");

            migrationBuilder.AddColumn<int>(
                name: "SecondRoundResultId",
                table: "FirstRoundResulAnswer",
                type: "int",
                nullable: true);

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
        }
    }
}
