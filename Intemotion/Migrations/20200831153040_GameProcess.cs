using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class GameProcess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameProcesses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameProcesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameProcesses_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConnectedUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectionId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    GameProcessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectedUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConnectedUsers_GameProcesses_GameProcessId",
                        column: x => x.GameProcessId,
                        principalTable: "GameProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConnectedUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConnectedUsers_GameProcessId",
                table: "ConnectedUsers",
                column: "GameProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectedUsers_UserId",
                table: "ConnectedUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameProcesses_GameId",
                table: "GameProcesses",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectedUsers");

            migrationBuilder.DropTable(
                name: "GameProcesses");
        }
    }
}
