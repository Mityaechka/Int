using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intemotion.Migrations
{
    public partial class ThirdRound_Result : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThirdRoundResultId",
                table: "GameProcesses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ThirdRoundResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionIndex = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    ThirdRoundState = table.Column<int>(nullable: false),
                    AnswerCloseTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdRoundResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssociationAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    AssociationVariantId = table.Column<int>(nullable: false),
                    ThirdRoundResultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociationAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssociationAnswer_AssociationVariant_AssociationVariantId",
                        column: x => x.AssociationVariantId,
                        principalTable: "AssociationVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssociationAnswer_ThirdRoundResult_ThirdRoundResultId",
                        column: x => x.ThirdRoundResultId,
                        principalTable: "ThirdRoundResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssociationAnswer_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChronologyAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    ChronologyItemId = table.Column<int>(nullable: false),
                    ThirdRoundResultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChronologyAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChronologyAnswer_ChronologyItem_ChronologyItemId",
                        column: x => x.ChronologyItemId,
                        principalTable: "ChronologyItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChronologyAnswer_ThirdRoundResult_ThirdRoundResultId",
                        column: x => x.ThirdRoundResultId,
                        principalTable: "ThirdRoundResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChronologyAnswer_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MelodyGuessAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    MelodyGuessVariantId = table.Column<int>(nullable: false),
                    ThirdRoundResultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MelodyGuessAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MelodyGuessAnswer_MelodyGuessVariant_MelodyGuessVariantId",
                        column: x => x.MelodyGuessVariantId,
                        principalTable: "MelodyGuessVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MelodyGuessAnswer_ThirdRoundResult_ThirdRoundResultId",
                        column: x => x.ThirdRoundResultId,
                        principalTable: "ThirdRoundResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MelodyGuessAnswer_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameProcesses_ThirdRoundResultId",
                table: "GameProcesses",
                column: "ThirdRoundResultId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociationAnswer_AssociationVariantId",
                table: "AssociationAnswer",
                column: "AssociationVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociationAnswer_ThirdRoundResultId",
                table: "AssociationAnswer",
                column: "ThirdRoundResultId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociationAnswer_UserId",
                table: "AssociationAnswer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChronologyAnswer_ChronologyItemId",
                table: "ChronologyAnswer",
                column: "ChronologyItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ChronologyAnswer_ThirdRoundResultId",
                table: "ChronologyAnswer",
                column: "ThirdRoundResultId");

            migrationBuilder.CreateIndex(
                name: "IX_ChronologyAnswer_UserId",
                table: "ChronologyAnswer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MelodyGuessAnswer_MelodyGuessVariantId",
                table: "MelodyGuessAnswer",
                column: "MelodyGuessVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_MelodyGuessAnswer_ThirdRoundResultId",
                table: "MelodyGuessAnswer",
                column: "ThirdRoundResultId");

            migrationBuilder.CreateIndex(
                name: "IX_MelodyGuessAnswer_UserId",
                table: "MelodyGuessAnswer",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameProcesses_ThirdRoundResult_ThirdRoundResultId",
                table: "GameProcesses",
                column: "ThirdRoundResultId",
                principalTable: "ThirdRoundResult",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameProcesses_ThirdRoundResult_ThirdRoundResultId",
                table: "GameProcesses");

            migrationBuilder.DropTable(
                name: "AssociationAnswer");

            migrationBuilder.DropTable(
                name: "ChronologyAnswer");

            migrationBuilder.DropTable(
                name: "MelodyGuessAnswer");

            migrationBuilder.DropTable(
                name: "ThirdRoundResult");

            migrationBuilder.DropIndex(
                name: "IX_GameProcesses_ThirdRoundResultId",
                table: "GameProcesses");

            migrationBuilder.DropColumn(
                name: "ThirdRoundResultId",
                table: "GameProcesses");
        }
    }
}
