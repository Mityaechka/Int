using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class ChatMessage_DeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_ConnectedUsers_ConnectedUserId",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "WebRTCOffer",
                table: "ConnectedUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_ConnectedUsers_ConnectedUserId",
                table: "ChatMessages",
                column: "ConnectedUserId",
                principalTable: "ConnectedUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_ConnectedUsers_ConnectedUserId",
                table: "ChatMessages");

            migrationBuilder.AddColumn<string>(
                name: "WebRTCOffer",
                table: "ConnectedUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_ConnectedUsers_ConnectedUserId",
                table: "ChatMessages",
                column: "ConnectedUserId",
                principalTable: "ConnectedUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
