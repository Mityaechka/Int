using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChronologyId",
                table: "ChronologyAnswer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChronologyAnswer_ChronologyId",
                table: "ChronologyAnswer",
                column: "ChronologyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChronologyAnswer_Chronology_ChronologyId",
                table: "ChronologyAnswer",
                column: "ChronologyId",
                principalTable: "Chronology",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChronologyAnswer_Chronology_ChronologyId",
                table: "ChronologyAnswer");

            migrationBuilder.DropIndex(
                name: "IX_ChronologyAnswer_ChronologyId",
                table: "ChronologyAnswer");

            migrationBuilder.DropColumn(
                name: "ChronologyId",
                table: "ChronologyAnswer");
        }
    }
}
