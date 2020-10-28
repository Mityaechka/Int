using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class WebRTC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WebRTCOffer",
                table: "ConnectedUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WebRTCOffer",
                table: "ConnectedUsers");
        }
    }
}
