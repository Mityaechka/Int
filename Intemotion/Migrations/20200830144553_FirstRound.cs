using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class FirstRound : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FirstRoundId",
                table: "Games",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FirstRound",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstRound", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntellectualQuestionCost",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false),
                    FirstRoundId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntellectualQuestionCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntellectualQuestionCost_FirstRound_FirstRoundId",
                        column: x => x.FirstRoundId,
                        principalTable: "FirstRound",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    FirstRoundId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionsCategory_FirstRound_FirstRoundId",
                        column: x => x.FirstRoundId,
                        principalTable: "FirstRound",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IntellectualQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false),
                    QuestionsCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntellectualQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntellectualQuestion_QuestionsCategory_QuestionsCategoryId",
                        column: x => x.QuestionsCategoryId,
                        principalTable: "QuestionsCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IntellectualAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: true),
                    IntellectualQuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntellectualAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntellectualAnswer_IntellectualQuestion_IntellectualQuestionId",
                        column: x => x.IntellectualQuestionId,
                        principalTable: "IntellectualQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_FirstRoundId",
                table: "Games",
                column: "FirstRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_IntellectualAnswer_IntellectualQuestionId",
                table: "IntellectualAnswer",
                column: "IntellectualQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_IntellectualQuestion_QuestionsCategoryId",
                table: "IntellectualQuestion",
                column: "QuestionsCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IntellectualQuestionCost_FirstRoundId",
                table: "IntellectualQuestionCost",
                column: "FirstRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsCategory_FirstRoundId",
                table: "QuestionsCategory",
                column: "FirstRoundId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_FirstRound_FirstRoundId",
                table: "Games",
                column: "FirstRoundId",
                principalTable: "FirstRound",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_FirstRound_FirstRoundId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "IntellectualAnswer");

            migrationBuilder.DropTable(
                name: "IntellectualQuestionCost");

            migrationBuilder.DropTable(
                name: "IntellectualQuestion");

            migrationBuilder.DropTable(
                name: "QuestionsCategory");

            migrationBuilder.DropTable(
                name: "FirstRound");

            migrationBuilder.DropIndex(
                name: "IX_Games_FirstRoundId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "FirstRoundId",
                table: "Games");
        }
    }
}
