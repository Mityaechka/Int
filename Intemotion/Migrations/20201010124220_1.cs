using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "MelodyGuessAnswer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "MelodyGuessAnswer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "MelodyGuessAnswer");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "MelodyGuessAnswer");
        }
    }
}
