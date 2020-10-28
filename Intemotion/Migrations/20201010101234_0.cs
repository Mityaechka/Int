using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class _0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChronologyAnswer_ChronologyItem_ChronologyItemId",
                table: "ChronologyAnswer");

            migrationBuilder.DropIndex(
                name: "IX_ChronologyAnswer_ChronologyItemId",
                table: "ChronologyAnswer");

            migrationBuilder.DropColumn(
                name: "ChronologyItemId",
                table: "ChronologyAnswer");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "ChronologyAnswer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "ChronologyAnswer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "ChronologyAnswer");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "ChronologyAnswer");

            migrationBuilder.AddColumn<int>(
                name: "ChronologyItemId",
                table: "ChronologyAnswer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChronologyAnswer_ChronologyItemId",
                table: "ChronologyAnswer",
                column: "ChronologyItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChronologyAnswer_ChronologyItem_ChronologyItemId",
                table: "ChronologyAnswer",
                column: "ChronologyItemId",
                principalTable: "ChronologyItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
