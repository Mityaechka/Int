using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class SecondRound : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SecondRoundId",
                table: "Games",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SecondRounds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondRounds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TruthQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: true),
                    TruthAnswer = table.Column<string>(nullable: true),
                    FalseAnswer = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false),
                    SecondRoundId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruthQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TruthQuestions_SecondRounds_SecondRoundId",
                        column: x => x.SecondRoundId,
                        principalTable: "SecondRounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_SecondRoundId",
                table: "Games",
                column: "SecondRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_TruthQuestions_SecondRoundId",
                table: "TruthQuestions",
                column: "SecondRoundId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_SecondRounds_SecondRoundId",
                table: "Games",
                column: "SecondRoundId",
                principalTable: "SecondRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_SecondRounds_SecondRoundId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "TruthQuestions");

            migrationBuilder.DropTable(
                name: "SecondRounds");

            migrationBuilder.DropIndex(
                name: "IX_Games_SecondRoundId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "SecondRoundId",
                table: "Games");
        }
    }
}
