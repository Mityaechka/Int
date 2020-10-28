using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "AssociationAnswer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "AssociationAnswer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "AssociationAnswer");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "AssociationAnswer");
        }
    }
}
