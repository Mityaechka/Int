using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class IsTruth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FalseAnswer",
                table: "TruthQuestions");

            migrationBuilder.DropColumn(
                name: "TruthAnswer",
                table: "TruthQuestions");

            migrationBuilder.AddColumn<bool>(
                name: "IsTruth",
                table: "TruthQuestions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTruth",
                table: "TruthQuestions");

            migrationBuilder.AddColumn<string>(
                name: "FalseAnswer",
                table: "TruthQuestions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TruthAnswer",
                table: "TruthQuestions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
